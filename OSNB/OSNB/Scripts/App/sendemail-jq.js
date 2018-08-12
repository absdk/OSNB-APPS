//-----------------------------------------------------
//start Add, Edit, Delete - Success Funtion

var sendEmailObjData;

$(function () {
    //start DataTable Script

    sendEmailObjData = $('#sendEmailDataTable').dataTable({
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
        "sAjaxSource": "/SendEmail/GetSendEmailInfos",
        "aoColumns": [
            { "sName": "SenderName" },
            { "sName": "SenderContactNo" },
            { "sName": "Subject" },
            { "sName": "Message" },
            { "sName": "MemberName" },
            { "sName": "Actions",
                "bSearchable": false,
                "bSortable": false,
                "sWidth": "30px",
                "fnRender": function (oObj) {
                    return '<a class="lnkDetailsSendEmail  btn btn-success btn-mini" style="margin-right: 5px;" href=\"/SendEmail/Details/' +
                                oObj.aData[5] + '\" ><icon class="icon-search icon-white"></icon></a>';

                }

            }
            ]
    });

    //end DataTable Script

    //-------------------------------------------------------
    //start Add, Edit, Delete - Dialog, Click Event

    //For details SendEmail
    $("#detailsSendEmailDialog").dialog({
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

    $('#sendEmailDataTable tbody td a.lnkDetailsSendEmail').live('click', function () {
        //$('#sendEmailDataTable tbody td .lnkDetailsSendEmail').on('click', 'a', function () {

        var linkObj = $(this);
        var dialogDiv = $('#detailsSendEmailDialog');
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