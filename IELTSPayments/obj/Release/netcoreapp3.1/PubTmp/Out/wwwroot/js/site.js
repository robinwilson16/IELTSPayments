$("#LoadingModal").modal("show");

//Load data in when model is displayed
$("#PaymentsModal").on("shown.bs.modal", function () {
    let transactionID = $("#TransactionID").val();
    let rootPath = $("#RootPath").val();
    if (rootPath == null) {
        rootPath = ``;
    }
    let dataToLoad = `${rootPath}/Transactions/Details/${transactionID}`;

    $.get(dataToLoad, function (data) {

    })
        .then(data => {
            var formData = $(data).find("#PaymentDetails");
            $("#PaymentDetails").html(formData);

            console.log(dataToLoad + " Loaded");
        })
        .fail(function () {
            let title = `Error Loading Payment Information`;
            let content = `The payment data at ${dataToLoad} returned a server error and could not be loaded`;

            doErrorModal(title, content);
        });
});

$("#PaymentsModal").on("hidden.bs.modal", function () {
    let loadingAnim = $("#LoadingHTML").html();

    $("#PaymentDetails").html(loadingAnim);
});

$(".SubmitOnEnter").keyup(function (event) {
    if ((event.keyCode || event.which) === 13) {
        //If enter key pressed
        $(".SearchTransactions").trigger("click");
    }
});

$(".SearchTransactions").click(function (event) {
    let rootPath = $("#RootPath").val();
    if (rootPath == null) {
        rootPath = ``;
    }
    let dataToLoad = `${rootPath}/Transactions?handler=Json`;
    let reportType = $("#ReportType").val();
    let britishCouncilRef = $("#BritishCouncilRef").val();
    let email = $("#Email").val();
    let paymentDateFrom = $("#PaymentDateFrom").val();
    let paymentDateTo = $("#PaymentDateTo").val();
    let actionsRequired = $("#ActionsRequired").is(":checked"); 
    let maxRecords = $("#MaxRecords").val();

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

    if (actionsRequired === true) {
        dataToLoad += `&actionsRequired=true`;
    }

    if (maxRecords.length > 0) {
        dataToLoad += `&maxRecords=${maxRecords}`;
    }

    $("#LoadingModal").modal("show");

    let listData = $("#TransactionList").DataTable();
    listData.ajax.url(dataToLoad).load(null, false);
    console.log(dataToLoad + " Loaded");
});

$(function () {
    //$.extend($.fn.dataTable.defaults, {
    //    language: {
    //        processing: '<div class="col text-center LoadingArea"><i class="fas fa-spinner fa-spin"></i></div>'
    //    }
    //});

    //var searchParams = $("#FilterQuery").val();
    var rootPath = $("#RootPath").val();
    if (rootPath == null) {
        rootPath = ``;
    }

    TransactionListDT = $('#TransactionList').DataTable({
        dom: '<"row"<"col-md"l><"col-md"f>>rt<"row"<"col-md"ip><"col-md text-right"B>>',
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
        //sDom: "fprtp", 
        processing: true,
        responsive: true, //Add this
        //language: {
        //    processing: '<i class="fa fa-spinner fa-spin fa-3x fa-fw"></i><span class="sr-only">Loading...</span>'
        //},
        serverSide: false,
        colReorder: true,
        deferRender: true,
        scroller: true,
        scrollY: 280,
        //ajax: { url: `${rootPath}/Transactions/?handler=Json&search=${searchParams}`, dataSrc: "" },
        ajax: {
            url: `${rootPath}/Transactions/?handler=Json`,
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
                    _: "bookSent",
                    sort: "bookSent",
                    filter: "bookSent",
                    display: trnActions
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
        <button type="button" class="btn btn-outline-primary btn-sm btn-block ViewFees" data-toggle="modal" data-target="#PaymentsModal" aria-label="${data.transactionID}">
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

function trnActions(data, type, dataToSet) {
    let itemsSent = ``;

    if (data.feeBooks > 0) {
        let bookChecked = ``;

        if (data.bookSent === true) {
            bookChecked = ` checked="checked"`;
        }

        itemsSent += `
            <i class="fas fa-book"></i> Book Sent
            <label class="switch-sm">
                <input type="checkbox" class="ItemSent Book" aria-label="${data.transactionID}"${bookChecked}>
                <span class="slider-sm round"></span>
            </label>`;
    }

    if (data.feeMockExam1 > 0 || data.feeMockExam2 > 0) {
        let dvdChecked = ``;

        if (data.dvdSent === true) {
            dvdChecked = ` checked="checked"`;
        }

        itemsSent += `
            <i class="fas fa-compact-disc"></i> DVD Sent
            <label class="switch-sm">
                <input type="checkbox" class="ItemSent DVD" aria-label="${data.transactionID}"${dvdChecked}>
                <span class="slider-sm round"></span>
            </label>`;
    }

    return itemsSent;
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

    $(".ViewFees").click(function (event) {
        let transactionID = $(this).attr("aria-label");
        $("#TransactionID").val(transactionID);
    });

    $(".ItemSent").click(function (event) {
        let transactionID = $(this).attr("aria-label");
        let isItemSent = $(this).is(":checked");
        let itemType = null;

        if ($(this).hasClass("Book")) {
            itemType = `Book`;
        }
        else if ($(this).hasClass("DVD")) {
            itemType = `DVD`;
        }

        if (itemType == null) {
            let title = `Error Updating Status`;
            let content = `It was not possible to determine what type of item was sent. Please try again`;

            //Set checkbox back as could not update database
            if (isItemSent === true) {
                $(this).prop('checked', false);
            }
            else {
                $(this).prop('checked', true);
            }

            doErrorModal(title, content);
        }
        else if (!transactionID > 0) {
            let title = `Error Updating Status`;
            let content = `It was not possible to determine which transaction needs to be updated. Please try again`;

            //Set checkbox back as could not update database
            if (isItemSent === true) {
                $(this).prop('checked', false);
            }
            else {
                $(this).prop('checked', true);
            }

            doErrorModal(title, content);
        }
        else {
            //alert(transactionID + ' - ' + itemType + ' - ' + isItemSent);
            saveItemSentStatus(transactionID, itemType, isItemSent);
        }
    });
}