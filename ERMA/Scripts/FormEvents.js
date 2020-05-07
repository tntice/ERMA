
$(document).ready(function () {
    
    $("#txtRequiredDate").datepicker({
        dateFormat: "MM/dd/yyyy",
        changeMonth: true,
        changeYear: true,
        yearRange: "-60:+10"
    });

    $("#txtRequiredDate").on("changeDate", function () {
        $(this).datepicker("hide");
    });

    $("#txtSearchVendor").on("keyup", function () {

        var searchvalue = (this).value;
        displaySearch(searchvalue);

    });

    $("#txtSearchVendorPart").on("keyup", function () {
        var searchvalue = (this).value;
        displayVendorPartSearch(searchvalue);

    });

    $("#txtGLAccount").on("change", function () {
        var GLAccountw = (this).value;
        var GLAccount = GLAccountw;
        if ((GLAccountw.length) > 2) {
            GLAccount = GLAccountw.substring(0, 2);
            if (GLAccount == "00") {
                GLAccount = GLAccountw.substring(2, 4);
            }
        }
        $("#txtDept").val(GLAccount);

    });

    $("#optProjectStatusActive").on("change", function () {

        document.body.style.cursor = "wait";
        var val = $("#ProjectGroupToProjectPGID").val();

        populateActiveProjects(val);
        document.body.style.cursor = "default";

    });

    $("#optProjectStatusAll").on("change", function () {

        document.body.style.cursor = "wait";
        var val = $("#ProjectGroupToProjectPGID").val();
        populateAllProjects(val);
        document.body.style.cursor = "default";
    });

    $("#optProjectStatusInactive").on("change", function () {

        document.body.style.cursor = "wait";
        var val = $("#ProjectGroupToProjectPGID").val();        
        populateInActiveProjects(val);
        document.body.style.cursor = "default";
    });

    $("#btnProjectGroupToProjectRemoveAll").on("click", function () {
        document.body.style.cursor = "wait";
    });

    $("#btnProjectGroupToProjectSubmit").on("click", function () {
        document.body.style.cursor = "wait";
    });

    $("#btnSelectVendor").on("click", function () {

        var venCode = $("#lstVendorSearchResults option:selected").val();
        $("#txtVendorCode").val(venCode);
    });

    $("#btnSelectVendorPart").on("click", function () {
        var venPart = $("#lstVendorPartSearchResults option:selected").val();
        $("#txtVendorPartNumber").val(venPart);

        displayVendorPartDescription(venPart);
    });

    $("#lstGLAccount").on("change", function () {
        var GLAccountw = (this).value;
        var GLAccount = GLAccountw;
        if ((GLAccountw.length) > 2) {
            GLAccount = GLAccountw.substring(0, 2);
            if (GLAccount != "00") {
                $("#txtDept").val(GLAccount);
            }
        }
    });

    $("#lstTaxGroup").on("change", function () {
        var tgroup = (this).value;
        populateTaxRate(tgroup);
    });

    $("#lstOrderUnit").on("change", function () {
        var uom = (this.value);
        $("#lstPriceUnit").val(uom);
    });

    $("#btnCreateReqLineItem").on("click", function () {
        document.body.style.cursor = "wait";
        TurnOnMasterSummary("true");

    });
    
    $("#btnMasterReqSubmit").on("click", function () {
        document.body.style.cursor = "wait";
    });

    $("#btnAddMasterReq").on("click", function () {

        $("#PriceSummary").hide();
        $("#LineItemPriceSummary").hide();
        $("#TotalPriceSummary").hide();
    });

    $("#liQty").on("change", function () {
        
        var qty = (this).value;
        var unitprice = $("#liUnitPrice").val();

        if (!(isNaN(qty)) && (!(isNaN(unitprice))) && (qty.toString().length > 0) && (unitprice.toString().length > 0)) {
            $("#PriceSummary").show();
            $("#LineItemPriceSummary").show();
            $("#TotalPriceSummary").show();
        }
        else {

            $("#LineItemPriceSummary").hide();
        }

    });

    $("#liUnitPrice").on("change", function () {
        var qty = $("#liQty").val();
        var unitprice = (this).value;

        if (!(isNaN(qty)) && (!(isNaN(unitprice))) && (qty.toString().length > 0) && (unitprice.toString().length > 0))
        {
            $("#PriceSummary").show();
            $("#LineItemPriceSummary").show();
            $("#TotalPriceSummary").show();
        }
        else {

            $("#LineItemPriceSummary").hide();
        }
    });


    

        //alert("Ready");
    $("#chMNGroupProjectAll").prop('checked', false);
    $("#chMNGroupProjectAll").click(function () {

        var status = $("#chMNGroupProjectAll").prop('checked');
            //alert("Status: " + status);
        toggleChecked(status);
    });

});

function toggleChecked(status) {
    //alert("Status: " + status);
    $("#checkboxes input").each(function () {
        $(this).prop("checked", status);
        //alert("Checkbox Name: " + $(this).prop("id"));
        //$(this).form.form.submit();
    });
}

function displaySearch(strValue)
{
    if (strValue.length > 2) {
        var subItems = "";
        var url = "../ReqDetail/GetVendorList/";

        $.getJSON(url, { VendorFilter:strValue }, function (data) {
            $.each(data, function (index, item) {
                subItems += "<option value='" + item.Value + "'>" + item.Text + "</option>";
            });
            $("#lstVendorSearchResults").html(subItems);
        });
    }
};

function displayVendorPartSearch(strValue)
{
    //if (strValue.length > 2) {
        var subItems = "";
        var url = "../ReqDetail/GetVendorPartList/";

        $.getJSON(url, { PartFilter: strValue }, function (data) {
            $.each(data, function (index, item) {
                subItems += "<option value='" + item.Value + "'>" + item.Text + "</option>";
            });
            $("#lstVendorPartSearchResults").html(subItems);
        });
    //}
}

function displayVendorPartDescription(strValue) {
    var url = "../ReqDetail/GetVendorPart/";
    if (strValue.length > 0) {
        $.getJSON(url, { PartNumber: strValue }, function (data) {
            $("#txtProductDescription").html(data);
        }
        );
    }
}

function populateTaxRate(strValue)
{
    var subItems = "";
    var url = "../ReqDetail/GetTaxRateList/";

    $.getJSON(url, { TaxGroup: strValue }, function (data) {
        $.each(data, function (index, item) {
            subItems += "<option value='" + item.Value + "'>" + item.Text + "</option>";
        });
        $("#lstTaxRate").html(subItems);
    });
}

function populateActiveProjects(strValue) {
    var subItems = "";
    var url = "../ProjectGroupToProject/GetActiveProjects/";

    $.getJSON(url, { PGID: strValue }, function (data) {
        $.each(data, function (index, item) {
            subItems += "<option value='" + item.Value + "'>" + item.Text + "</option>";
        });
        $("#ddProjectGroupToProjectList").html(subItems);
    });
}

function populateInActiveProjects(strValue) {
    var subItems = "";
    var url = "../ProjectGroupToProject/GetInActiveProjects/";

    $.getJSON(url, { PGID: strValue }, function (data) {
        $.each(data, function (index, item) {
            subItems += "<option value='" + item.Value + "'>" + item.Text + "</option>";
        });
        $("#ddProjectGroupToProjectList").html(subItems);
    });
}


function populateAllProjects(strValue) {
    var subItems = "";
    var url = "../ProjectGroupToProject/GetAllProjects/";

    $.getJSON(url, { PGID: strValue }, function (data) {
        $.each(data, function (index, item) {
            subItems += "<option value='" + item.Value + "'>" + item.Text + "</option>";
        });
        $("#ddProjectGroupToProjectList").html(subItems);
    });
}

function TurnOnMasterSummary(toogle) {

    //alert("Page Load");
    if (toogle == "true") {

        $("#LineItemPriceSummary").hide();
        $("#TotalPriceSummary").show();
        $("#PriceSummary").show();
    } else {

        $("#LineItemPriceSummary").hide();
        $("#TotalPriceSummary").hide();
        $("#PriceSummary").hide();
    }
}