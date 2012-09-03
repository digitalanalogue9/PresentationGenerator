/// <reference path="../jquery-1.8.0-vsdoc.js" />
/// <reference path="../jQuery.tmpl.js" />
/// <reference path="../jquery-ui-1.8.23.js" />
/// <reference path="models.js" />
/// <reference path="../json2.js" />
/// <reference path="../jquery.validate.js" />

var pageNumber = 0;

function jsguidGenerator() {
    var S4 = function() {
       return (((1+Math.random())*0x10000)|0).toString(16).substring(1);
    };
    return (S4()+S4()+"-"+S4()+"-"+S4()+"-"+S4()+"-"+S4()+S4()+S4());
}

function guidGenerator(){
    var id;
        $.ajax({
          url: "/Presentation/GuidGenerator",
          type: "GET",
          dataType: "json",
          contentType: "application/json; charset=utf-8",
          cache: false,
          async: false
        })
        .done(function(data) { 
            id = data.Id;
        })
        .fail(function() { alert("error"); })
        .always(function() {  });
    return id;
}


$(function () {
    var $tabs = $("#tabs");
    $tabs.tabs({
        tabTemplate: "<li><a href='#{href}'><input type='text' name='Pages[#{label}].Id' id='Pages_#{label}_Id' class='tabid input-small' value='Page #{label}'/></a> <span class='ui-icon ui-icon-close'>Remove Tab</span></li>",
        add: function (event, ui) {
            $tabs.tabs('select', '#' + ui.panel.id);
        }
    }).find(".ui-tabs-nav").sortable({ axis: "x" });
    $("body").on("click", "#newpage", function () {
        pageNumber += 1;
        addPage(pageNumber);
    });


    // close icon: removing the tab on click
    $("body").on("click", "#tabs span.ui-icon-close", function () {
        var index = $("li", $tabs).index($(this).parent());
        if (index != 0) {
            $tabs.tabs("remove", index);
        }
    });

    $("body").on("click", "#viewbutton", function () {

        window.open("/Presentation/View/" + $("#PresentationId").val(), "_blank", "fullscreen=yes,toolbar=no,menubar=no", false);
    });

    $("body").on("click", "#savebutton", function () {
        if ($("#makeform").valid()) {
            var pages = [];
            var id = guidGenerator();
            $('#tabs .ui-tabs-nav a').each(function (index, value) {
                var tabid = $(this).attr('href').replace("#page-", "");

                $(".pagecontent", "#page-" + tabid).htmlarea("updateTextArea");
                var id = $(this).find("input:first").val().replace(" ", "");
                var css = $("#Pages_" + tabid + "_CSS").val();
                var rotate = $("#Pages_" + tabid + "_Rotate").val();
                var scale = $("#Pages_" + tabid + "_Scale").val();
                var x = $("#Pages_" + tabid + "_X").val();
                var y = $("#Pages_" + tabid + "_Y").val();
                var z = $("#Pages_" + tabid + "_Z").val();
                var content = $(".pagecontent", "#page-" + tabid).htmlarea("toHtmlString");
                if (content == null) {
                    content = "<div>Needs Content!</div>";
                }
                //            var page = new Page(id, css, x, y, z, rotate, scale, content);
                pages.push({ Id: id, CSS: css, Rotate: rotate, Scale: scale, Content: content == null ? "" : content, X: x, Y: y, Z: z });
            });

            var presentation = {
                Id: id,
                Title: $("#Title").val(),
                Pages: pages
            };

            $.ajax({
                url: "/Presentation/Make",
                type: "POST",
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify(presentation),
                cache: false
            })
            .done(function () {
                if ($("#PresentationId").val().length == 0) {
                    $("#PresentationId").val(id);
                }
                $("#viewbutton").removeClass("hide");
            })
            .fail(function () { alert("error"); })
            .always(function () { });
        }
        else {
            $("#systemmessage").append("<div class='alert'>A field is empty - sort it!</div>");
//            window.setTimeout(function () {
//                $("#systemmessage div").fadeTo(500, 0).slideUp(500, function () {
//                    $("#systemmessage div").remove();
//                });

//            }, 5000);
        }
        return false;
    });

    /* Start Here */
    $("#makeform").validate();
    if ($("#PresentationId").val().length > 0) {
        $("#viewbutton").removeClass("hide");
        $(".knob").knob();
        $(".pagecontent").htmlarea();
        $("div.jHtmlArea").css("width", "80%");
        $("div.jHtmlArea div.ToolBar").css("width", "78%");
        $("div.jHtmlArea div:nth-child(2) iframe").css("width", "100%").css("height", "250px");
    }
    else {
        addPage(pageNumber);
    }



    function addPage(index) {
        $tabs.tabs("add", "#page-" + index.toString(), (index + 1).toString());
        $("#tabtemplate").tmpl({ Index: index }).appendTo("#page-" + index.toString());
        $(".knob", "#page-" + index.toString()).knob();
        $(".pagecontent", "#page-" + index.toString()).htmlarea();
        $("div.jHtmlArea", "#page-" + index.toString()).css("width", "80%");
        $("div.jHtmlArea div.ToolBar", "#page-" + index.toString()).css("width", "78%");
        $("div.jHtmlArea div:nth-child(2) iframe", "#page-" + index.toString()).css("width", "100%").css("height", "250px");

    }



    //    $(".pagecontent").markItUp(mySettings);
    //    $(".knob", "#page-0").knob({
    //        /*change : function (value) {
    //        //console.log("change : " + value);
    //        },
    //        release : function (value) {
    //        console.log("release : " + value);
    //        },
    //        cancel : function () {
    //        console.log("cancel : " + this.value);
    //        },*/
    //        draw: function () {
    //            // "tron" case
    //            if (this.$.data('skin') == 'tron') {

    //                var a = this.angle(this.cv)  // Angle
    //                                , sa = this.startAngle          // Previous start angle
    //                                , sat = this.startAngle         // Start angle
    //                                , ea                            // Previous end angle
    //                                , eat = sat + a                 // End angle
    //                                , r = 1;

    //                this.g.lineWidth = this.lineWidth;

    //                this.o.cursor
    //                                && (sat = eat - 0.3)
    //                                && (eat = eat + 0.3);

    //                if (this.o.displayPrevious) {
    //                    ea = this.startAngle + this.angle(this.v);
    //                    this.o.cursor
    //                                    && (sa = ea - 0.3)
    //                                    && (ea = ea + 0.3);
    //                    this.g.beginPath();
    //                    this.g.strokeStyle = this.pColor;
    //                    this.g.arc(this.xy, this.xy, this.radius - this.lineWidth, sa, ea, false);
    //                    this.g.stroke();
    //                }

    //                this.g.beginPath();
    //                this.g.strokeStyle = r ? this.o.fgColor : this.fgColor;
    //                this.g.arc(this.xy, this.xy, this.radius - this.lineWidth, sat, eat, false);
    //                this.g.stroke();

    //                this.g.lineWidth = 2;
    //                this.g.beginPath();
    //                this.g.strokeStyle = this.o.fgColor;
    //                this.g.arc(this.xy, this.xy, this.radius - this.lineWidth + 1 + this.lineWidth * 2 / 3, 0, 2 * Math.PI, false);
    //                this.g.stroke();

    //                return false;
    //            }
    //        }
    //    });

});

