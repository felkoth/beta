
function pageLoad() {
   
    // Hide notification on mouseleave
    $('#notification').mouseleave(function () {
        $("#notification").addClass("hidden");
    });

 }// End pageLoad

 function filter(txt, _id, cellNr) {
     var search = txt.value.toLowerCase();
     var table = document.getElementById(_id);

     if (table != null) {
         var ele;
         for (var r = 1; r < table.rows.length; r++) {
             ele = table.rows[r].cells[cellNr].innerHTML.replace(/<[^>]+>/g, "");
             if (ele.toLowerCase().indexOf(search) >= 0)
                 table.rows[r].style.display = '';
             else table.rows[r].style.display = 'none';
         }
     }
 } // END FILTER