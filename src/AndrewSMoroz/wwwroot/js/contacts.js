
//=====================================================================================
// This ready() method demonstrates a way to show a confirmation dialog using jquery-ui
//=====================================================================================

$(document).ready(function () {

    var attributeID = "data-delete-item-id";            // Put this in both the <a> for the delete action, and in the corresponding <span> that contains the custom text that should be displayed in the confirmation dialog
    var dialogID = "dialog-confirm-delete";             // ID of the confirmation dialog element
    var dialogTextID = "dialog-confirm-delete-text";    // ID of an element on the confirmation dialog where custom text can be placed

    //--------------------------------------------------------------------------------------------------------------
    // Initialize the confirmation dialog
    $("#" + dialogID).dialog({
        autoOpen: false,
        modal: true,
        width: 400
    });

    //--------------------------------------------------------------------------------------------------------------
    // This click event will display the confirmation dialog and either follow the link or cancel that action
    $("a[" + attributeID + "]").click(function (e) {

        e.preventDefault();
        var $anchor = $(this);                                                          // Will be an <a> element
        var $span = $("span[" + attributeID + "=" + $anchor.attr(attributeID) + "]")    // Corresponding <span> element containing text to include in conformation dialog

        $("span#" + dialogTextID).text($span.text());                                   // Put extra text into the the dialog

        $("#" + dialogID).dialog({
            dialogClass: "no-close",
            buttons: {
                "Confirm": function () {
                    window.location.href = $anchor.attr("href");
                },
                "Cancel": function () {
                    $(this).dialog("close");
                }
            }
        });

        $("#" + dialogID).dialog("open");

    });

});


//================================================================================
// The below is another way to show a confirmation dialog, using native javascript
//================================================================================

////------------------------------------------------------------------------------------------------------------------
//$(document).ready(function () {

//    var attributeID = "data-delete-item-id";

//    //--------------------------------------------------------------------------------------------------------------
//    function ConfirmDelete() {
//        var $anchor = $(this);                                                          // Will be an <a> element
//        var $span = $("span[" + attributeID + "=" + $anchor.attr(attributeID) + "]")    // Corresponding <span> element containing text to include in conformation dialog
//        var confirmText = "Please confirm that you want to delete this item: \n\n" + $span.text();
//        if (!confirm(confirmText)) {
//            $("[data-toggle='tooltip']").tooltip("hide");
//            return false;                                                               // Don't follow the link
//        }
//    }

//    //--------------------------------------------------------------------------------------------------------------
//    $("a[" + attributeID + "]").click(ConfirmDelete);

//});
