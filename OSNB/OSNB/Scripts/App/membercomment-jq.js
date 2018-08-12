//-----------------------------------------------------
//start Edit, Delete - Success Funtion

// Edit Comment Success Function
function EditCommentSuccess() {
    if ($("#updateTargetId").html() == "True") {

        //now we can close the dialog
        $('#editCommentDialog').dialog('close');

        //JQDialogAlert mass, status
        JQDialogAlert("Comment updated successfully.", "dialogSuccess");

        commentObjData.fnDraw();

    }
    else {
        //show message in popup
        $("#updateTargetId").show();
    }
}

// Delete Comment Success Function
function DeleteCommentSuccess() {
    if ($("#updateTargetId").html() == "True") {

        //now we can close the dialog
        $('#deleteCommentDailog').dialog('close');

        //JQDialogAlert mass, status
        JQDialogAlert("Comment deleted successfully.", "dialogSuccess");

        commentObjData.fnDraw();

    }
    else {
        //show message in popup
        $("#updateTargetId").show();

    }
}
//end Add, Edit, Delete - Success Funtion
//-----------------------------------------------------

var commentObjData;

$(function () {
    //start DataTable Script

    commentObjData = $('#commentDataTable').dataTable({
        "bJQueryUI": false,
        "bAutoWidth": false,
        "sPaginationType": "bootstrap",
        "bSort": false,
        "oLanguage": {
            "sLengthMenu": "_MENU_ items per page",
            "sZeroRecords": "Data not found",
            "sProcessing": "Loading...",
            "sInfo": "_START_ - _END_ of _TOTAL_ items",
            "sInfoEmpty": "0 - 0 of 0 items",
            "sInfoFiltered": "(filtered from _MAX_ total items)"
        },
        "bProcessing": true,
        "bServerSide": true,
        "sAjaxSource": "/Admin/Comment/GetComments",
        "aoColumns": [
            { "sName": "Name" },
            { "sName": "Email" },
            { "sName": "Description" },
            { "sName": "CreateDate" },
            { "sName": "Actions",
                "bSearchable": false,
                "bSortable": false,
                "sWidth": "85px",
                "fnRender": function (oObj) {
                    return '<a class="lnkDetailsComment btn btn-primary btn-mini" style="margin-right: 5px;" href=\"/Admin/Comment/Details/' +
                                oObj.aData[4] + '\" ><icon class="icon-search icon-white"></icon></a>' +
                                '<a class="lnkEditComment  btn btn-success btn-mini" style="margin-right: 5px;" href=\"/Admin/Comment/Edit/' +
                                oObj.aData[4] + '\" ><icon class="icon-pencil icon-white"></icon></a>' +
                                '<a class="lnkDeleteComment btn btn-danger btn-mini" href=\"/Admin/Comment/Delete/' +
                                oObj.aData[4] + '\" ><icon class="icon-trash icon-white"></icon></a>';

                }

            }
            ]
    });

    //end DataTable Script

    //-------------------------------------------------------
    //start Edit, Delete - Dialog, Click Event

    //edit Comment
    $("#editCommentDialog").dialog({
        autoOpen: false,
        width: 500,
        resizable: false,
        closeOnEscape: false,
        modal: true,
        close: function (event, ui) {
            $(".popover").hide();
        },
        buttons: {
            "Edit": function () {
                //make sure there is nothing on the message before we continue   
                $("#updateTargetId").html('');
                $("#editCommentForm").submit();
            },
            "Cancel": function () {
                $(this).dialog("close");
            }
        }

    });

    $('#commentDataTable tbody td a.lnkEditComment').live('click', function () {
        //$('#commentDataTable tbody td .lnkEditComment').on('click', 'a', function () {

        //change the title of the dialog
        var linkObj = $(this);
        var dialogDiv = $('#editCommentDialog');
        var viewUrl = linkObj.attr('href');
        $.get(viewUrl, function (data) {
            dialogDiv.html(data);
            //validation
            var $form = $("#editCommentForm");
            // Unbind existing validation
            $form.unbind();
            $form.data("validator", null);
            // Check document for changes
            $.validator.unobtrusive.parse(document);
            // Re add validation with changes
            $form.validate($form.data("unobtrusiveValidation").options);
            //open dialog
            dialogDiv.dialog('open');
        });
        return false;

    });

    //delete Comment
    $("#deleteCommentDailog").dialog({
        autoOpen: false,
        width: 400,
        resizable: false,
        modal: true,
        buttons: {
            "Yes": function () {
                //make sure there is nothing on the message before we continue                         
                $("#updateTargetId").html('');
                $("#deleteCommentForm").submit();
            },
            "Cancel": function () {
                $(this).dialog("close");
            }
        }
    });

    $('#commentDataTable tbody td a.lnkDeleteComment').live('click', function () {
        //$('#commentDataTable tbody td .lnkDeleteComment').on('click', 'a', function () {

        //change the title of the dialog
        var linkObj = $(this);
        var dialogDiv = $('#deleteCommentDailog');
        var viewUrl = linkObj.attr('href');
        $.get(viewUrl, function (data) {
            dialogDiv.html(data);
            //validation
            var $form = $("#deleteCommentForm");
            // Unbind existing validation
            $form.unbind();
            $form.data("validator", null);
            // Check document for changes
            $.validator.unobtrusive.parse(document);
            // Re add validation with changes
            $form.validate($form.data("unobtrusiveValidation").options);
            //open dialog
            dialogDiv.dialog('open');
        });
        return false;

    });

    //For details Comment
    $("#detailsCommentDialog").dialog({
        autoOpen: false,
        width: 500,
        resizable: false,
        modal: true,
        buttons: {
            "Cancel": function () {
                $(this).dialog("close");
            }
        }
    });

    $('#commentDataTable tbody td a.lnkDetailsComment').live('click', function () {
        //$('#commentDataTable tbody td .lnkDetailsComment').on('click', 'a', function () {

        var linkObj = $(this);
        var dialogDiv = $('#detailsCommentDialog');
        var viewUrl = linkObj.attr('href');
        $.get(viewUrl, function (data) {
            dialogDiv.html(data);
            dialogDiv.dialog('open');
        });
        return false;

    });

    //end Add, Edit, Delete - Dialog, Click Event
    //-------------------------------------------------------

});