async function saveItemSentStatus(transactionID, itemType, isItemSent) {
    return new Promise(resolve => {
        var antiForgeryTokenID = $("#AntiForgeryTokenID").val();
        let rootPath = $("#RootPath").val();
        if (rootPath == null) {
            rootPath = ``;
        }
        let dataToLoad = `${rootPath}/Transactions/UpdateStatus/${transactionID}`;

        let bookSent = null;
        let DVDSent = null;
        let itemSentDesc = null;

        if (itemType === "Book") {
            bookSent = isItemSent;
        }
        else if (itemType === "DVD") {
            DVDSent = isItemSent;
        }

        if (isItemSent === true) {
            itemSentDesc = `been sent`;
        }
        else {
            itemSentDesc = `not been sent`;
        }

        //alert(`TransactionID: ${transactionID} IsItemSent: ${isItemSent} IsBookSent: ${bookSent} IsDVDSent: ${DVDSent}`);

        var params = {
            type: "POST",
            url: dataToLoad,
            beforeSend: function (xhr) {
                xhr.setRequestHeader("RequestVerificationToken", antiForgeryTokenID);
            },
            success: function (data) {
                var audio = new Audio(`${rootPath}/sounds/confirm.wav`);
                audio.play();
                console.log(`${itemType} successfully recorded as having ${itemSentDesc}" for transaction "${transactionID}"`);
                resolve(1);
            },
            error: function (error) {
                //doCrashModal(error);
                console.error(`Error recording that ${itemType} has "${itemSentDesc} for transaction "${transactionID}`);
                //reject(0);
                resolve(0);
            }
        };

        params.data = {
            'IELTSTransaction.TransactionID': transactionID,
            'IELTSTransaction.BookSent': bookSent,
            'IELTSTransaction.DVDSent': DVDSent,
            '__RequestVerificationToken': antiForgeryTokenID
        };

        $.ajax(params);
    });
}