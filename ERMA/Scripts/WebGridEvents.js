


$(document).ready(function () {

    //alert("Works");
    
    //Loop through all Child Grids.
    $("#WebGrid .ChildGrid").each(function () {
        //Copy the Child Grid to DIV.
        var childGrid = $(this).clone();
        $(this).closest("TR").find("TD").eq(0).find("DIV").append(childGrid);

        //Remove the Last Column from the Row.
        $(this).parent().remove();
    });

    //Remove Last Column from Header Row.
    $("#WebGrid TH:last-child").eq(0).remove();


    //Assign Click event to Plus Image.
    $("body").on("click", "img[src*='plus.png']", function () {
        //alert("Clicked Plus Sign");
        $(this).closest("tr").after("<tr><td></td><td colspan = '999'>" + $(this).next().html() + "</td></tr>");
        $(this).attr("src", "/image/minus.png");
    });

    //Assign Click event to Minus Image.
    $("body").on("click", "img[src*='minus.png']", function () {
        //alert("Clicked Minus Sign");
        $(this).attr("src", "/image/plus.png");
        $(this).closest("tr").next().remove();
    });


    $("body").on("click", ".save-approval", function () {

        var tr = $(this).parents('tr:first');
        var reqid = tr.children('td:first').text();
        var opt = "approvalGroup" + reqid;
        var resn = "denyreason" + reqid;
        var bnAction = "bnApprove" + reqid;

        var indx = $("input[name=" + resn + "]").length - 1;

        var optChk = $("input[name=" + opt + "]:checked").val();
        var reason = $("input[name=" + resn + "]")[indx].value;
        
        if (optChk == null) {
            alert("Please select an action");
        } else {
            if (reason.length == 0 && optChk == "Deny")
            {
                alert("Please enter a denial reason");
            }
            else
            {
                //alert("Opt Checked: " + optChk + ". Reason: " + reason);

                var ApproveModel =
                    {
                        "ID": reqid,
                        "Action": optChk,
                        "Reason": reason
                    };

                $.ajax({
                    url: "../Route/ProcessApproval",
                    data: JSON.stringify(ApproveModel),
                    type: "Post",
                    contentType: "application/json; charset=utf-8",
                    success: function (data) {
                        alert(data);
                    }
                });

                $("button[name=" + bnAction + "]")[indx].disabled = true;
                $("input[name=" + resn + "]")[indx].disabled = true;
                $("input[name=" + opt + "]")[2].disabled = true;
                $("input[name=" + opt + "]")[3].disabled = true;
                
            }
        }
    });


});
//}