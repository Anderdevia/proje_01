//! moment.js locale configuration
//! locale : Spanish(United State) [es-us]
//! author : bustta : https://github.com/bustta

; (function (global, factory) {
    typeof exports === 'object' && typeof module !== 'undefined'
        && typeof require === 'function' ? factory(require('../moment')) :
    typeof define === 'function' && define.amd ? define(['../moment'], factory) :
    factory(global.moment)
}(this, (function (moment) {
    'use strict';


    var monthsShortDot = 'Ene._Feb._Mar._Abr._May._Jun._Jul._Ago._Sep._Oct._Nov._Dic.'.split('_');
    var monthsShort = 'Ene_Feb_Mar_Abr_May_Jun_Jul_Ago_Sep_Oct_Nov_Dic'.split('_');

    var esUs = moment.defineLocale('es-us', {
        months: 'Enero_Febrero_Marzo_Abril_Mayo_Junio_Julio_Agosto_Septiembre_Octubre_Noviembre_Diciembre'.split('_'),
        monthsShort: function (m, format) {
            if (!m) {
                return monthsShortDot;
            } else if (/-MMM-/.test(format)) {
                return monthsShort[m.month()];
            } else {
                return monthsShortDot[m.month()];
            }
        },
        monthsParseExact: true,
        weekdays: 'Domingo_Lunes_Martes_Miércoles_Jueves_Viernes_Sábado'.split('_'),
        weekdaysShort: 'Dom._Lun._Mar._Mié._Jue._Vie._Sáb.'.split('_'),
        weekdaysMin: 'Do_Lu_Ma_Mi_Ju_Vi_Sá'.split('_'),
        weekdaysParseExact: true,
        longDateFormat: {
            LT: 'H:mm',
            LTS: 'H:mm:ss',
            L: 'MM/DD/YYYY',
            Z: 'YYYY-MM-DD',
            ZZ: 'DD/MM/YYYY',
            LL: 'MMMM D [de] YYYY',
            LLL: 'MMMM  D [del] YYYY H:mm',
            LLLL: 'dddd, MMMM [de] D [de] YYYY H:mm'
        },
        calendar: {
            sameDay: function () {
                return '[Hoy a la' + ((this.hours() !== 1) ? 's' : '') + '] LT';
            },
            nextDay: function () {
                return '[Mañana a la' + ((this.hours() !== 1) ? 's' : '') + '] LT';
            },
            nextWeek: function () {
                return 'dddd [a la' + ((this.hours() !== 1) ? 's' : '') + '] LT';
            },
            lastDay: function () {
                return '[Ayer a la' + ((this.hours() !== 1) ? 's' : '') + '] LT';
            },
            lastWeek: function () {
                return '[el] dddd [pasado a la' + ((this.hours() !== 1) ? 's' : '') + '] LT';
            },
            sameElse: 'L'
        },
        relativeTime: {
            future: 'En %s',
            past: 'Hace %s',
            s: 'Unos segundos',
            m: 'Un minuto',
            mm: '%d minutos',
            h: 'Una hora',
            hh: '%d horas',
            d: 'Un día',
            dd: '%d días',
            M: 'Un mes',
            MM: '%d meses',
            y: 'Un año',
            yy: '%d años'
        },
        dayOfMonthOrdinalParse: /\d{1,2}º/,
        ordinal: '%dº',
        week: {
            dow: 0, // Sunday is the first day of the week.
            doy: 6  // The week that contains Jan 1st is the first week of the year.
        }
    });

    return esUs;

})));
