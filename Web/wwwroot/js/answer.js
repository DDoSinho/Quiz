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

        var div = $("<div class='answer' id='" + randomID + "'></div>");
        var textfield = $("<input type='text' placeholder='Answer text' name='Answer[" + index + "].Text' required/>");
        var checkbox = $("<input class='checkbox' type='checkbox' value='true' name='Answer[" + index + "].IsGoodAnswer'/>");
        var button = $("<button type='button'>Remove</button>");
        var hiddenTextfield = $("<input type='text' value=" + questionId + " name='Answer[" + index + "].QuestionId' hidden='hidden'/>");

        var answerLine = div.append(textfield).append(checkbox).append(button).append(hiddenTextfield);

        $("#form").prepend(answerLine);

        button.on("click", function (e) {
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


        if (countCheckBoxes > 0)
        {
            $(this).submit(); //if the submit button can not submit because of preventDefault
        }
        else {
            e.preventDefault(); //if the user does not check any checkbox, we dont have to submit
            var errorMessage= $("<div>You have to give at least one good answer!</div>")
            $("#submitdiv").append(errorMessage);
        }

    });
});