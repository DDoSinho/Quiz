$(document).ready(function () {

    function guid() {
        function s4() {
            return Math.floor((1 + Math.random()) * 0x10000)
                .toString(16)
                .substring(1);
        }
        return s4() + s4() + '-' + s4() + '-' + s4() + '-' +
            s4() + '-' + s4() + s4() + s4();
    }


    var index = 1;
    var questionId = $("#questionid").val();


    $("#add").on('click', function (e) {

        var randomID = guid();

        var rowdiv = $(
            `<div id=` + randomID + ` style="margin-top: 5px">
            <div class="row">
                <div class="col-lg-6">
                    <div class="input-group">
                        <span class="input-group-addon">
                            <input type="checkbox" class="checkbox" value="true" name="Answer[` + index + `].IsGoodAnswer" aria-label="...">
                        </span>
                        <input type="text" class="form-control" placeholder="Answer text" name="Answer[` + index + `].Text" aria-label="..." style="width:200px;" required> 
                        <button type="button" class="btn btn-lg btn-default" id="remove`+ index + `" style="padding: 4px 16px; margin-left: 10px; color: #fff; background-color: #333">Remove</button>
                    </div>
                </div>
            </div>
                 <input type="text" value="`+questionId+`" name="Answer[` + index + `].QuestionId" hidden="hidden" id="questionid" />
            </div>
        `);


        $("#form").prepend(rowdiv);

        $("#remove" + index).on("click", function (e) {
            $("#" + randomID).hide();
            $("#" + randomID + " input").val(" ");
        });

        index++;
    });

    $("#remove").on('click', function (e) {
        $("#firstanswer").hide();
        $("#firstanswer input").val(" ");
    });



    $("#addanswersbutton").on('click', function (e) {

        var countCheckBoxes = 0;

        $("input.checkbox").each(function (i) {
            if ($(this).is(":visible") && $(this).is(":checked")) {
                ++countCheckBoxes;
            }
        });


        if (countCheckBoxes > 0) {
            $(this).submit(); //if the submit button can not submit because of preventDefault
        }
        else {
            e.preventDefault(); //if the user does not check any checkbox, we dont have to submit
            var errorMessage = $("<div>You have to give at least one good answer!</div>")
            $("#submitdiv").append(errorMessage);
        }

    });
});