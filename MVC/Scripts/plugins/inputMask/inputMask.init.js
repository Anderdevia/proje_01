$(function(){
	$(".numberFormat").inputmask("numeric", {
		radixPoint: ".",
		groupSeparator: ",",
		digits: 2,
		autoGroup: true,
		prefix: '',
		rightAlign: false,
		oncleared: function () { }
	});

    $.fn.rVal=jQuery.fn.val;
    $.fn.val=function(value) {
        if (value != undefined) {
            if($(this).hasClass('numberFormat')){
                value = typeof value === 'number' ?
                    value.toFixed(2).replace(/(\d)(?=(\d{3})+\.)/g, '$1,'):
                    typeof value === 'string' ?
                    parseFloat(value.toString().replace(/[a-z- $,]/g, '') * 1) : 0;
                value = (!value) ? 0 : value;
            }
            return this.rVal(value);
            }
        if(this[0]) 
            return ($(this[0]).hasClass('numberFormat')) ? ($(this[0]).rVal().replace(/,/gi, "").length!=0)?parseFloat($(this[0]).rVal().replace(/,/gi,"")):0 : $(this[0]).rVal();
        return undefined;
    };
});