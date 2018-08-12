//-----------------------------------------------------
//start Add, Edit, Delete - Success Funtion
// Add Post Success Function
function AddPostSuccess() {

    if ($("#updateTargetId").html() == "True") {

        //now we can close the dialog
        $('#addPostDialog').dialog('close');

        //JQDialogAlert mass, status
        JQDialogAlert("Post saved successfully.", "dialogSuccess");

        postObjData.fnDraw();

    }
    else {
        //show message in popup
        $("#updateTargetId").show();
    }
}

// Edit Post Success Function
function EditPostSuccess() {
    if ($("#updateTargetId").html() == "True") {

        //now we can close the dialog
        $('#editPostDialog').dialog('close');

        //JQDialogAlert mass, status
        JQDialogAlert("Post updated successfully.", "dialogSuccess");

        postObjData.fnDraw();

    }
    else {
        //show message in popup
        $("#updateTargetId").show();
    }
}

// Delete Post Success Function
function DeletePostSuccess() {
    if ($("#updateTargetId").html() == "True") {

        //now we can close the dialog
        $('#deletePostDailog').dialog('close');

        //JQDialogAlert mass, status
        JQDialogAlert("Post deleted successfully.", "dialogSuccess");

        postObjData.fnDraw();

    }
    else {
        //show message in popup
        $("#updateTargetId").show();

    }
}
//end Add, Edit, Delete - Success Funtion
//-----------------------------------------------------

var postObjData;

$(function () {
    //start DataTable Script

    postObjData = $('#postDataTable').dataTable({
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
        "sAjaxSource": "/Admin/Post/GetPosts",
        "aoColumns": [
            { "sName": "Title" },
            { "sName": "Content" },
            { "sName": "CreateDate" },
            { "sName": "UserName" },
            { "sName": "Actions",
                "bSearchable": false,
                "bSortable": false,
                "sWidth": "90px",
                "fnRender": function (oObj) {
                    return '<a class="lnkDetailsPost btn btn-primary btn-mini" style="margin-right: 5px;" href=\"/Admin/Post/Details/' +
                                oObj.aData[4] + '\" ><icon class="icon-search icon-white"></icon></a>' +
                                '<a class="lnkEditPost  btn btn-success btn-mini" style="margin-right: 5px;" href=\"/Admin/Post/Edit/' +
                                oObj.aData[4] + '\" ><icon class="icon-pencil icon-white"></icon></a>' +
                                '<a class="lnkDeletePost btn btn-danger btn-mini" href=\"/Admin/Post/Delete/' +
                                oObj.aData[4] + '\" ><icon class="icon-trash icon-white"></icon></a>';

                }

            }
            ]
    });

    //end DataTable Script

    //-------------------------------------------------------
    //start Add, Edit, Delete - Dialog, Click Event

    $("#addPostDialog").dialog({
        autoOpen: false,
        width: 500,
        resizable: false,
        modal: true,
        buttons: {
            "Add": function () {
                //make sure there is nothing on the message before we continue 
                $("#updateTargetId").html('');
                $("#addPostForm").submit();
            },
            "Cancel": function () {
                $(this).dialog("close");
            }
        }
    });

    //add Post
    $('#lnkAddPost').click(function () {

        //change the title of the dialog
        var linkObj = $(this);
        var dialogDiv = $('#addPostDialog');
        var viewUrl = linkObj.attr('href');

        $.get(viewUrl, function (data) {
            dialogDiv.html(data);
            //validation
            var $form = $("#addPostForm");
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

    //edit Post
    $("#editPostDialog").dialog({
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
                $("#editPostForm").submit();
            },
            "Cancel": function () {
                $(this).dialog("close");
            }
        }

    });

    $('#postDataTable tbody td a.lnkEditPost').live('click', function () {
        //$('#postDataTable tbody td .lnkEditPost').on('click', 'a', function () {

        //change the title of the dialog
        var linkObj = $(this);
        var dialogDiv = $('#editPostDialog');
        var viewUrl = linkObj.attr('href');
        $.get(viewUrl, function (data) {
            dialogDiv.html(data);
            //validation
            var $form = $("#editPostForm");
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

    //delete Post
    $("#deletePostDailog").dialog({
        autoOpen: false,
        width: 400,
        resizable: false,
        modal: true,
        buttons: {
            "Yes": function () {
                //make sure there is nothing on the message before we continue                         
                $("#updateTargetId").html('');
                $("#deletePostForm").submit();
            },
            "Cancel": function () {
                $(this).dialog("close");
            }
        }
    });

    $('#postDataTable tbody td a.lnkDeletePost').live('click', function () {
        //$('#postDataTable tbody td .lnkDeletePost').on('click', 'a', function () {

        //change the title of the dialog
        var linkObj = $(this);
        var dialogDiv = $('#deletePostDailog');
        var viewUrl = linkObj.attr('href');
        $.get(viewUrl, function (data) {
            dialogDiv.html(data);
            //validation
            var $form = $("#deletePostForm");
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

    //For details Post
    $("#detailsPostDialog").dialog({
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

    $('#postDataTable tbody td a.lnkDetailsPost').live('click', function () {
        //$('#postDataTable tbody td .lnkDetailsPost').on('click', 'a', function () {

        var linkObj = $(this);
        var dialogDiv = $('#detailsPostDialog');
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