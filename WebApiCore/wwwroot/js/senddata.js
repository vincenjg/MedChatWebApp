var result = '[[{"type":"text","label":"step 1","className":"form-control","name":"text-1545217902718","subtype":"text"}],[{"type":"text","label":"step 2","className":"form-control","name":"text-1545217905331","subtype":"text"}],[{"type":"text","label":"step 3","className":"form-control","name":"text-1545217906652","subtype":"text"}],[{"type":"text","label":"step 4","className":"form-control","name":"text-1545217908197","subtype":"text"}]]';

// result contains multi-tab json data.

var res = jQuery.parseJSON(result);
var stepLen = res.length;
var j;
for (var i = 1; i <= stepLen; i++) {
    j = i + 1;
    var final_tab_id = j
    var tabId = "step-" + j.toString();

    var $newPageTemplate = $(document.getElementById("new-step"));
    var $newPage = $newPageTemplate.clone().attr("id", tabId).addClass("fb-editor");
    var $newTab = $('#add-step-tab').clone().removeAttr("id");
    var $tabLink = $("a", $newTab).attr("href", "#" + tabId).text("Step " + j);

    $newPage.insertBefore($newPageTemplate);
    $newTab.insertBefore('#add-step-tab');
    $fbPages.tabs("refresh");
    $fbPages.tabs("option", "active", i);
    fbInstances.push($newPage.formBuilder(fbOptions));
    formBuilder2.actions.setData(res[i - 1])
}