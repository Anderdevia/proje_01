var table = $('#dataTable').DataTable({
    language: {
        url: "/Scripts/plugins/dataTables/es-CO.json"
    },
    scrollX: true,
    pageLength: 10
});

$('#dataTable tbody').on('click', 'tr', function () {
    if ($(this).hasClass('selected')) {
        $(this).removeClass('selected');
    }
    else {
        table.$('tr.selected').removeClass('selected');
        $(this).addClass('selected');
    }
});

$.fn.dataTable.Api.register('row.addByPos()', function (data, index) {
    var currentPage = this.page();
    //insert the row 
    this.row.add(data);
    //move added row to desired index
    var rowCount = this.data().length - 1,
        insertedRow = this.row(rowCount).data(),
        tempRow;
    for (var i = rowCount; i >= index; i--) {
        tempRow = this.row(i - 1).data();
        this.row(i).data(tempRow);
        this.row(i - 1).data(insertedRow);
    }
    //refresh the current page
    this.page(currentPage).draw(false);
});

