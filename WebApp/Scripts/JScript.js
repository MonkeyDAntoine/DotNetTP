$(document).ready(function(){

    $("#Bold_Btn").on('click', function(e){
        e.preventDefault();
        $.ajax({
            type: "POST",
            url: "Index.aspx/BoldText",
            data: '{text : "'+$('#SourceCode_TextBox').textrange('get','text')+'"}',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function(response) {
                $('#SourceCode_TextBox').textrange('replace', response.d);
             console.log(response);
             return false;
            },
            error : function(response) {
                console.log(response);
            },
            failure: function(response) {
                console.log(response);
            }
        });
    });
    
    $("#Italic_Btn").on('click', function(e){
        e.preventDefault();
        $.ajax({
            type: "POST",
            url: "Index.aspx/ItalicText",
            data: '{text : "'+$('#SourceCode_TextBox').textrange('get','text')+'"}',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function(response) {
                $('#SourceCode_TextBox').textrange('replace', response.d);
             console.log(response);
             return false;
            },
            error : function(response) {
                console.log(response);
            },
            failure: function(response) {
                console.log(response);
            }
        });
    });
    
});