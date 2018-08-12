
// Edit AdminBloodRequest Success Function
function StatusAdminBloodRequestSuccess() {
    if ($("#updateTargetId").html() == "True") {

        //now we can close the dialog
        $('#statusBloodRequestDialog').dialog('close');

        //JQDialogAlert mass, status
        JQDialogAlert("Blood Request updated successfully.", "dialogSuccess");

        bloodRequestObjData.fnDraw();

    }
    else {
        //show message in popup
        $("#updateTargetId").show();
    }
}


//-----------------------------------------------------
//start Add, Edit, Delete - Success Funtion

var bloodRequestObjData;

$(function () {
    //start DataTable Script

    bloodRequestObjData = $('#bloodRequestDataTable').dataTable({
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
        "sAjaxSource": "/Admin/AdminBloodRequest/GetBloodRequests",
        "aoColumns": [
            { "sName": "RequesterName" },
            { "sName": "RequesterContactNo" },
            { "sName": "RequesterAmount" },
            { "sName": "PresentLocation" },
            { "sName": "DateOfDonation" },
            { "sName": "RequiredBloodGroup" },
            { "sName": "RequesterStatus" },
            { "sName": "Actions",
                "bSearchable": false,
                "bSortable": false,
                "sWidth": "70px",
                "fnRender": function (oObj) {
                    return '<a class="lnkDetailsBloodRequest  btn btn-success btn-mini" style="margin-right: 5px;" href=\"/Admin/AdminBloodRequest/Details/' +
                                oObj.aData[7] + '\" ><icon class="icon-search icon-white"></icon></a> <a class="lnkStatusBloodRequest  btn btn-search btn-mini" style="margin-right: 5px;" href=\"/Admin/AdminBloodRequest/Status/' +
                                oObj.aData[7] + '\" ><icon class="icon-pencil icon-white"></icon></a>';

                }

            }
            ]
    });

    //end DataTable Script

    //-------------------------------------------------------
    //start Add, Edit, Delete - Dialog, Click Event

    //For details BloodRequest
    $("#detailsBloodRequestDialog").dialog({
        autoOpen: false,
        width: 600,
        resizable: false,
        modal: true,
        buttons: {
            "Cancel": function () {
                $(this).dialog("close");
            }
        }
    });

    $('#bloodRequestDataTable tbody td a.lnkDetailsBloodRequest').live('click', function () {
        //$('#bloodRequestDataTable tbody td .lnkDetailsBloodRequest').on('click', 'a', function () {

        var linkObj = $(this);
        var dialogDiv = $('#detailsBloodRequestDialog');
        var viewUrl = linkObj.attr('href');
        $.get(viewUrl, function (data) {
            dialogDiv.html(data);
            dialogDiv.dialog('open');
        });
        return false;

    });


    //status AdminBloodRequest
    $("#statusBloodRequestDialog").dialog({
        autoOpen: false,
        width: 500,
        resizable: false,
        closeOnEscape: false,
        modal: true,
        close: function (event, ui) {
            $(".popover").hide();
        },
        buttons: {
            "Update": function () {
                //make sure there is nothing on the message before we continue   
                $("#updateTargetId").html('');
                $("#statusAdminBloodRequestForm").submit();
            },
            "Cancel": function () {
                $(this).dialog("close");
            }
        }

    });

    $('#bloodRequestDataTable tbody td a.lnkStatusBloodRequest').live('click', function () {
        //$('#bloodRequestDataTable tbody td .lnkStatusBloodRequest').on('click', 'a', function () {

        //change the title of the dialog
        var linkObj = $(this);
        var dialogDiv = $('#statusBloodRequestDialog');
        var viewUrl = linkObj.attr('href');
        $.get(viewUrl, function (data) {
            dialogDiv.html(data);
            //validation
            var $form = $("#statusAdminBloodRequestForm");
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