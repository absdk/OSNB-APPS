//-----------------------------------------------------
//start Add, Edit, Delete - Success Funtion

// SendEmail Donar Success Function
function SendEmailDonarSuccess() {
    if ($("#updateTargetId").html() == "True") {

        //now we can close the dialog
        $('#deleteDonarDailog').dialog('close');

        //JQDialogAlert mass, status
        JQDialogAlert("Donar deleted successfully.", "dialogSuccess");

        bloodGroupInfoObjData.fnDraw();

    }
    else {
        //show message in popup
        $("#updateTargetId").show();

    }
}
//end SendEmail - Success Funtion
//-----------------------------------------------------

var bloodGroupInfoObjData;

$(function () {
    //start DataTable Script

    var bloodGroupId = $("#BloodGroupId").val();

    bloodGroupInfoObjData = $('#bloodGroupInfoDataTable').dataTable({
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
        "bServerSide": false,
        "sAjaxSource": "/Donar/GetDonarByBloodGroupList/" + bloodGroupId,
        "aoColumns": [
            { "sName": "FullName" },
            { "sName": "Address" },
            { "sName": "DateOfBirth" },
            { "sName": "PhoneNumber" },
            { "sName": "MobileNumber" },
            { "sName": "Actions",
                "bSearchable": false,
                "bSortable": false,
                "sWidth": "90px",
                "fnRender": function (oObj) {
                    return '<a class="lnkSendEmailDonar btn btn-primary btn-mini" style="margin-right: 5px;" href=\"/Donar/SendEmail/' +
                                oObj.aData[6] + '\" ><icon class="icon-envelope icon-white"></icon></a>' +
                                '<a class="lnkDetailDonar  btn btn-success btn-mini" style="margin-right: 5px;" href=\"/Donar/Details/' +
                                oObj.aData[6] + '\" ><icon class="icon-search icon-white"></icon></a>';

                }

            }
            ]
    });

    //end DataTable Script

    //-------------------------------------------------------
    //start Add, Edit, Delete - Dialog, Click Event

    //SendEmail Donar
    $("#sendEmailDonarDailog").dialog({
        autoOpen: false,
        width: 600,
        resizable: false,
        modal: true,
        buttons: {
            "Send": function () {
                //make sure there is nothing on the message before we continue                         
                $("#updateTargetId").html('');
                $("#sendEmailDonarForm").submit();
            },
            "Cancel": function () {
                $(this).dialog("close");
            }
        }
    });

    $('#bloodGroupInfoDataTable tbody td a.lnkSendEmailDonar').live('click', function () {
        //$('#bloodGroupInfoDataTable tbody td .lnkSendEmailDonar').on('click', 'a', function () {

        //change the title of the dialog
        var linkObj = $(this);
        var dialogDiv = $('#sendEmailDonarDailog');
        var viewUrl = linkObj.attr('href');
        $.get(viewUrl, function (data) {
            dialogDiv.html(data);
            //validation
            var $form = $("#sendEmailDonarForm");
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

    //end Add, Edit, Delete - Dialog, Click Event
    //-------------------------------------------------------

});