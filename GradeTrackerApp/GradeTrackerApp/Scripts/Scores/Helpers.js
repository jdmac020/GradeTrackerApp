function triggerConfirmButton(passedScoreId) {
    
    var deleteLink = document.getElementsByClassName('a.delete-link');
    deleteLink.hide();
    var confirmButton = deleteLink.siblings(".delete-confirm");
    confirmButton.show();
    
    var showDeleteLink = function () {
        confirmButton.hide();
        deleteLink.show();
    };

    var onKeyPress = function (e) {
        //Cancel if escape key pressed
        if (e.which === 27) {
            cancelDelete();
        }
    };
    
    var deleteItem = function () {

        window.location.href = '@Url.Action("DeleteScore", routeValues: new {scoreId = passedScoreId})';

    };

    var removeEvents = function () {
        confirmButton.off("click", deleteItem);
        $(document).on("click", cancelDelete);
        $(document).off("keypress", onKeyPress);
    };

    var cancelDelete = function () {
        removeEvents();
        showDeleteLink();
    };

    confirmButton.on("click", deleteItem);
    $(document).on("click", cancelDelete);
    $(document).on("keypress", onKeyPress);

}
