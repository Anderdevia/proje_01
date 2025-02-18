/**
 * FileUpload 2.0.5
 * Requiere las siguientes referencias:
 *
 * CSS:
 * ~/Templates/Velonic/assets/fileupload/fileupload.css
 *
 * JS:
 * ~/Scripts/loadingoverlay.min.js
 * ~/Scripts/Utilidades.js
 * ~/Templates/Velonic/assets/fileupload/fileupload.js
 */
class FileUpload {
    /**
     * Verifica el tipo y tamaño del archivo cargado antes de ser enviado al servidor.
     * @param {string} inputFile Selector del input file a validar.
     * @param {function} xhrSuccess Callback invocado cuando la petición HTTP es exitosa.
     * @param {function} xhrBefore Callback invocado antes de realizar la petición HTTP.
     */
    constructor(inputFile, xhrSuccess, xhrBefore) {
        let _files = [];
        let _requestSize = 0;
        let _inputFile = $(inputFile);
        let _fileUpload = _inputFile.parent();
        let _infoUpload = $("~ blockquote :nth-child(3)", _fileUpload);
        let _validationMsg = $("[data-valmsg-for=" + _inputFile.attr("id") + "]");
        let _labelInputFile = $("[for=" + _inputFile.attr("id") + "]:not(.fileUpload)");
        let _validExtension = _inputFile.attr("accept").replace(/[\s\.]/g, "").toLowerCase().split(",");

        _fileUpload.on("dragover dragenter", function () {
            $(this).addClass("fileUploadHover");
        });

        _fileUpload.on("dragleave dragend drop", function () {
            $(this).removeClass("fileUploadHover");
        });

        _fileUpload.on("drop", main);

        _inputFile.change(function (e) {
            var target = e.target || e.srcElement;
            if (target.value.length > 0)
                main(e);
        });

        _fileUpload.next().click(function () {
            _files = [];
            _requestSize = 0;
            _inputFile.val("");
            _infoUpload.text("0 Bytes");
            $("div", _fileUpload).remove();
            $(this).hide();
            clean();
        });

        _fileUpload.on("click", ".fa-trash-o", function (e) {
            e.preventDefault();
            var i = $(this).closest("div").attr("id").replace("file_", "").trim();

            _requestSize -= _files[i].size;
            _files.splice(i, 1);
            _inputFile.val("");
            _infoUpload.text(formatBytes(_requestSize, 0));
            $("#file_" + i).remove();

            $("> div", _fileUpload).each(function (i) {
                $(this).attr("id", "file_" + i);
            });
            if (_files.length < 2)
                _fileUpload.next().hide();
            if (_requestSize < _inputFile.attr("data-max-size") && !$("progress").hasClass("fileInvalid"))
                clean();
            else
                errorValidate();
        });

        _inputFile.closest("form").submit(function (e) {
            e.preventDefault();

            if (_inputFile.attr("required") && $(_files).length == 0 || _fileUpload.hasClass("fileUploadInvalid"))
                return false;
            
            var form = this,
                data = new FormData(form);

            $(_files).each(function () {
                data.append(_inputFile.attr("id"), this);
            });
            $.ajax({
                url: form.action,
                dataType: "json",
                type: form.method,
                contentType: false,
                cache: false,
                data: data,
                processData: false,
                beforeSend: xhrBefore || function () {
                    $.LoadingOverlay("show");
                },
                success: xhrSuccess || function (result, textStatus, jqXHR) {
                    if (result.success)
                        //$.Notification.autoHideNotify("success", "top right", "Correcto!", result.message);
                        toastr.error(result.message, '¡Algo salió mal!', toastr.options = { progressBar: true, closeButton: true })
                    else {
                        console.log(result.message);
                        toastr.error(result.message, '¡Algo salió mal!', toastr.options = { progressBar: true, closeButton: true })
                        //swal("Algo salió mal", result.message, "error");
                    }
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    console.log(textStatus);
                    toastr.error(textStatus, '¡Algo salió mal!', toastr.options = { progressBar: true, closeButton: true })
                },
                complete: function () {
                    $.LoadingOverlay("hide");
                }
            });
        });

        /**
         * Método principal que gestiona la lectura y validación de los archivos seleccionados.
         * @param {JQueryEventObject} e Evento que contiene los archivos seleccionados.
         */
        function main(e) {
            var newFiles = e.target.files || e.originalEvent.dataTransfer.files,
                oldLength = _files.length;
            clean();

            if ($(_inputFile).attr("multiple")) {
                _files.push.apply(_files, newFiles);
                if (_files.length > 1)
                    _fileUpload.next().show();
            }
            else {
                oldLength = 0;
                _requestSize = 0;
                $("div", _fileUpload).remove();
                newFiles = Array.prototype.slice.call(newFiles, 0, 1);
                _files = newFiles;
            }
            $(newFiles).each(function (i, file) {
                var fileReader = new FileReader();
                _requestSize += file.size;
                i += oldLength;
                $("hr", _fileUpload).before("<div id='file_" + i + "'><cite><b>" + file.name + "</b></cite><strong>" + formatBytes(file.size, 0) + "</strong><progress max='100'></progress></div>");

                fileReader.onloadstart = function (e) {
                    $.LoadingOverlay("show");
                    if (!file.size)
                        e.target.abort();
                };
                fileReader.onprogress = function (e) {
                    if (e.lengthComputable)
                        $("#file_" + i + " progress").attr("value", parseInt(((e.loaded / e.total) * 100), 10));
                };
                fileReader.onload = function (e) {
                    successHandler(e, i, file);
                };
                fileReader.onloadend = function (e) {
                    $("#file_" + i + " strong").after("<a class='fa fa-trash-o fa-lg' data-toggle='tooltip' data-placement='top' data-original-title='Eliminar'></a>");
                    $("#file_" + i + " progress").attr("value", parseInt(((e.loaded / e.total) * 100), 10));
                    $("[data-toggle=tooltip]").tooltip();
                    $.LoadingOverlay("hide");
                };
                fileReader.onerror = function (e) {
                    errorHandler(e, i, file.name);
                };
                if (file.type.match("text/plain") || file.name.split(".").pop().toLowerCase() == "txt")
                    fileReader.readAsText(this);
                else
                    fileReader.readAsArrayBuffer(this);
            });
            _infoUpload.text(formatBytes(_requestSize, 0));
            if (_requestSize >= _inputFile.attr("data-max-size") || $("progress").hasClass("fileInvalid"))
                errorValidate();
        }

        /**
         * Valida un archivo que haya sido leído correctamente, de acuerdo a su extensión y firma digital.
         * @param {Event} e Evento con la información de una lectura de archivo exitosa.
         * @param {number} i Índice del archivo del listado de archivos a validar.
         * @param {File} file Archivo a validar.
         */
        function successHandler(e, i, file) {
            var content = e.target.result,
                extension = file.name.split(".").pop().toLowerCase();

            if (file.type.match("text/plain") || extension == "txt") {
                var cont = 0,
                    code = 0;

                for (let j = 0; j < content.length; j++) {
                    code = content.substr(j, 1).charCodeAt(0);
                    if (code < 32 && code !== 9 && code !== 10 && code !== 13) {
                        cont++;
                        break;
                    }
                }
                if (cont > 0) {
                    errorValidate();
                    $("#file_" + i + " progress").addClass("fileInvalid");
                }
            }
            else {
                var magicNumber = "";

                $((new Uint8Array(content)).subarray(0, 4)).each(function () {
                    magicNumber += this.toString(16);
                });
                if (_validExtension.indexOf(extension) > -1 && validMagicNumber(extension).indexOf(magicNumber) > -1) {
                    if (file.type.match(/image.*/))
                        thumbnail(i, file);
                }
                else {
                    errorValidate();
                    $("#file_" + i + " progress").addClass("fileInvalid");
                }
            }
            /**
             * Remover comentario para obtener la Firma del Archivo a validar, para más
             * información visita https://en.wikipedia.org/wiki/List_of_file_signatures
             */
            //console.log("Type: " + file.type + " Magic number: " + magicNumber);
        }

        /**
         * Gestiona los posibles errores que se puedan presentar al leer un archivo.
         * @param {ErrorEvent} e Evento de error que contiene información del mismo.
         * @param {number} i Índice del archivo del listado de archivos a validar.
         * @param {string} name Nombre del arhivo a validar.
         */
        function errorHandler(e, i, name) {
            var msg = "";

            switch (e.target.error.code) {
                case e.target.error.NOT_FOUND_ERR:
                    msg = "El archivo " + name + " no se encontró";
                break;
                case e.target.error.NOT_READABLE_ERR:
                    msg = "El archivo " + name + " no es legible";
                break;
                case e.target.error.SECURITY_ERR:
                    msg = "El archivo " + name + " se ha bloqueado por seguridad";
                break;
                case e.target.error.ENCODING_ERR:
                    msg = "El archivo " + name + " es demasiado grande para ser codificado";
                break;
                case e.target.error.ABORT_ERR:
                    msg = "La carga del archivo " + name + " se ha cancelado";
                break;
                default:
                    msg = "Ha ocurrido un error leyendo el archivo " + name;
            }
            errorValidate();
            $("#file_" + i + " progress").addClass("fileInvalid");
            toastr.error(msg, '¡Algo salió mal!', toastr.options = { progressBar: true, closeButton: true })
            // swal("Algo salió mal", msg, "error");
        }

        /**
         * Retorna la Firma del Archivo correspondiente a su extensión.
         * @param {string} extension Extensión del archivo a validar.
         */
        function validMagicNumber(extension) {
            switch (extension) {
                case "jpg":
                case "jpeg":
                    return ["ffd8ffe0", "ffd8ffe1"];
                case "png":
                    return ["89504e47"];
                case "csv":
                    return ["44657061"];
                case "pdf":
                    return ["25504446"];
                case "rar":
                    return ["52617221"];
                case "zip":
                case "docx":
                case "odt":
                case "xlsx":
                case "ods":
                case "pptx":
                    return ["504b34"];
                case "doc":
                case "xls":
                case "ppt":
                    return ["d0cf11e0"];
                default:
                    return [];
            }
        }

        /**
         * Crea la miniatura de una imagen cargada.
         * @param {number} i Índice del archivo del listado de archivos a validar.
         * @param {File} file Archivo de imagen a generar miniatura.
         */
        function thumbnail(i, file) {
            var fileReader = new FileReader();

            fileReader.onload = function () {
                $("#file_" + i).css({ "background-image": "url(" + fileReader.result + ")", "cursor": "pointer" }).click(function (e) {
                    e.preventDefault();
                    var target = e.target || e.srcElement;

                    if (target == this)
                        return window.open("", "_blank").document.write("<img src='" + fileReader.result + "' />");
                });
            };
            fileReader.readAsDataURL(file);
        }

        /**
         * Establece el estado inválido del FileUpload.
         */
        function errorValidate() {
            if (!_fileUpload.hasClass("fileUploadInvalid")) {
                _fileUpload.addClass("fileUploadInvalid");
                _labelInputFile.addClass("invalid");
                _validationMsg.text("Se han encontrado archivos inválidos y/o supera máximo tamaño");
            }
        }

        /**
         * Remueve el estado inválido del FileUpload.
         */
        function clean() {
            _fileUpload.removeClass("fileUploadInvalid");
            _labelInputFile.removeClass("invalid");
            _validationMsg.text("");
        }

        function formatBytes(bytes, decimals) {
            if (bytes == 0) return "0 Bytes";
            var k = 1000,
                dm = decimals + 1 || 3,
                sizes = ["Bytes", "KB", "MB", "GB", "TB", "PB", "EB", "ZB", "YB"],
                i = Math.floor(Math.log(bytes) / Math.log(k));
            return parseFloat((bytes / Math.pow(k, i)).toFixed(dm)) + " " + sizes[i];
        }
    }
}


