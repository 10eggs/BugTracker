Snippets:

## populate ddl ##

            //Populate drop down list for ticket details
            //if (ticketStatusDDL.is(':empty')) {
            //    console.log('EMPTY!');
            //    $.each(response.ticketStatusType, function (key, entry) {
            //        ticketStatusDDL.append($('<option></option>').attr('value', key).text(entry));
            //    })
            //}

## Pass serialized array ##
//function objectifyForm(formArray) {//serialize data function
//    var returnArray = {};
//    for (var i = 0; i < formArray.length; i++) {
//        returnArray[formArray[i]['name']] = formArray[i]['value'];
//    }
//    return returnArray;



//public static class UrlBuilderExtensions{
public static UrlBuilder id(this UrlBuilder urlBuilder)=> urlBuilder.Add()
}

public class UrlBuilder{
    public List<string> FirledsToInclude = new();
    public UrlBuilder Add([CallerMemberName] string callSite = null)
    {
        FieldsToInclude.Add(callSite);
        return this;
    }
}