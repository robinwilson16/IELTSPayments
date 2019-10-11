$("#LoadingModal").modal("show");

$(".SubmitOnEnter").keyup(function (event) {
    if ((event.keyCode || event.which) === 13) {
        //If enter key pressed
        $(".SearchTransactions").trigger("click");
    }
});

$(".SearchTransactions").click(function (event) {
    let dataToLoad = `/Transactions?handler=Json`;
    let reportType = $("#ReportType").val();
    let britishCouncilRef = $("#BritishCouncilRef").val();
    let email = $("#Email").val();
    let paymentDateFrom = $("#PaymentDateFrom").val();
    let paymentDateTo = $("#PaymentDateTo").val();

    if (reportType.length > 0) {
        dataToLoad += `&reportType=${reportType}`;
    }

    if (britishCouncilRef.length > 0) {
        dataToLoad += `&britishCouncilRef=${britishCouncilRef}`;
    }

    if (email.length > 0) {
        dataToLoad += `&email=${email}`;
    }

    if (paymentDateFrom.length > 0) {
        dataToLoad += `&paymentDateFrom=${paymentDateFrom}`;
    }

    if (paymentDateTo.length > 0) {
        dataToLoad += `&paymentDateTo=${paymentDateTo}`;
    }

    $("#LoadingModal").modal("show");

    let listData = $("#TransactionList").DataTable();
    listData.ajax.url(dataToLoad).load(null, false);
    console.log(dataToLoad + " Loaded");
});

$(".ViewFees").click(function (event) {
    alert("Search button clicked");
});

$(".SendEmail").click(function (event) {
    alert("Search button clicked");
});

$(function () {
    //$.extend($.fn.dataTable.defaults, {
    //    language: {
    //        processing: '<div class="col text-center LoadingArea"><i class="fas fa-spinner fa-spin"></i></div>'
    //    }
    //});

    //var searchParams = $("#FilterQuery").val();

    TransactionListDT = $('#TransactionList').DataTable({
        dom: '<"row"<"col-sm-12 col-md-6"l><"col-sm-12 col-md-6"f>>rt<"row"<"col-md text-right"B>><"row"<"col-sm-12 col-md-5"i><"col-sm-12 col-md-7"p>>',
        buttons: [
            {
                extend: 'colvis',
                text: '<i class="fas fa-columns"></i> Columns'
            },
            {
                extend: 'copyHtml5',
                text: '<i class="far fa-copy"></i> Copy',
                exportOptions: {
                    columns: ':visible'
                }
            },
            {
                extend: 'excelHtml5',
                text: '<i class="far fa-file-excel"></i> Excel',
                exportOptions: {
                    columns: ':visible'
                }
            },
            {
                extend: 'csvHtml5',
                text: '<i class="fas fa-file-csv"></i> CSV',
                exportOptions: {
                    columns: ':visible'
                }
            },
            {
                extend: 'pdfHtml5',
                text: '<i class="far fa-file-pdf"></i> PDF',
                exportOptions: {
                    columns: ':visible'
                }
            },
            {
                extend: 'print',
                text: '<i class="fas fa-print"></i> Print',
                exportOptions: {
                    columns: ':visible'
                }
            }
        ],
        sDom: "prtp", 
        processing: true,
        responsive: true, //Add this
        //language: {
        //    processing: '<i class="fa fa-spinner fa-spin fa-3x fa-fw"></i><span class="sr-only">Loading...</span>'
        //},
        serverSide: false,
        colReorder: true,
        deferRender: true,
        scroller: true,
        scrollY: 420,
        //ajax: { url: "/Transactions/?handler=Json&search=" + searchParams, dataSrc: "" },
        ajax: {
            url: "/Transactions/?handler=Json",
            dataSrc: ""
        },
        columns: [
            {
                data: {
                    _: "britishCouncilRef",
                    sort: "britishCouncilRef",
                    filter: "britishCouncilRef"
                }
            },
            {
                data: {
                    _: "surname",
                    sort: "surname",
                    filter: "surname"
                }
            },
            {
                data: {
                    _: "forename",
                    sort: "forename",
                    filter: "forename"
                }
            },
            {
                data: {
                    _: "paymentDate",
                    sort: "paymentDate",
                    filter: "paymentDate",
                    display: trnPaymentDate
                }
            },
            {
                data: {
                    _: "email",
                    sort: "email",
                    filter: "email"
                }
            },
            {
                data: {
                    _: "feeExam",
                    sort: "feeExam",
                    filter: "feeExam"
                },
                visible: false
            },
            {
                data: {
                    _: "feeTotal",
                    sort: "feeTotal",
                    filter: "feeTotal",
                    display: trnFeeTotal
                }
            },
            {
                data: {
                    _: "paymentStatus",
                    sort: "paymentStatus",
                    filter: "paymentStatus",
                    display: trnPaymentStatus
                }
            },
            {
                data: {
                    _: "emailProgress",
                    sort: "emailProgress",
                    filter: "emailProgress"
                }
            },
            {
                data: {
                    _: "emailProgress",
                    sort: "emailProgress",
                    filter: "emailProgress",
                    display: trnSendEmail
                }
            }
        ],
        //order: [[3, "asc"], [4, "asc"], [2, "asc"]],
        order: [],
        drawCallback: function (settings, json) {
            attachListFunctions();
        }
    });
});

function trnPaymentDate(data, type, dataToSet) {
    return `
        ${moment(data.paymentDate).format('DD MMM YY')}`;
}

function trnFeeTotal(data, type, dataToSet) {
    return `
        <button type="button" class="btn btn-outline-primary btn-sm btn-block ViewFees" data-toggle="modal" data-target="#PaymentsModal">
            <i class="fas fa-search-dollar"></i> ${formatMoney(data.feeTotal, 0, "£")}
        </button>`;
}

function trnPaymentStatus(data, type, dataToSet) {
    let transactionClass = "PositiveTransaction";
    if (data.paymentStatus === "DECLINED") {
        transactionClass = "DeclinedTransaction";
    }

    return `
        <div class="${transactionClass}">
            ${data.paymentStatus}
        </div>`;
}

function trnSendEmail(data, type, dataToSet) {
    return `
        <i class="fas fa-book"></i> Book Sent
        <label class="switch-sm">
            <input type="checkbox" class="BookSent">
            <span class="slider-sm round"></span>
        </label>

        <i class="fas fa-compact-disc"></i> DVD Sent
        <label class="switch-sm">
            <input type="checkbox" class="BookSent">
            <span class="slider-sm round"></span>
        </label>`;
}

function formatMoney(num, rnd, symb, decimalSep, thousSep) {
    rnd = isNaN(rnd = Math.abs(rnd)) ? 2 : rnd;
    symb = symb === undefined ? "." : symb;
    decimalSep = decimalSep === undefined ? "." : decimalSep;
    thousSep = thousSep === undefined ? "," : thousSep;

    var s = num < 0 ? "-" : "";
    var i = String(parseInt(num = Math.abs(Number(num) || 0).toFixed(rnd)));
    var j = (j = i.length) > 3 ? j % 3 : 0;

    return s + symb + (j ? i.substr(0, j) + thousSep : "") + i.substr(j).replace(/(\d{3})(?=\d)/g, "$1" + thousSep) + (rnd ? decimalSep + Math.abs(num - i).toFixed(rnd).slice(2) : "");
}

function attachListFunctions() {
    //Attach after table has finished loading
    $("#LoadingModal").modal("hide");
}