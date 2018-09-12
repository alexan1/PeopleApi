function ViewModel() {
    
}

// Activates knockout.js
ko.applyBindings(new ViewModel());


function getPerson(qid) {

    var url = "https://query.wikidata.org/sparql";

    var query = 'SELECT DISTINCT  ?item ?itemLabel ?itemDescription (SAMPLE(?DR) AS ?DRSample) (SAMPLE(?article) AS ?articleSample)'
        + 'WHERE{ ?article  schema:about       ?item ; schema:inLanguage  "en" ; schema:isPartOf    <https://en.wikipedia.org/>'
        + 'FILTER ( ?item = <http://www.wikidata.org/entity/Q' + qid + '> )'
        + 'OPTIONAL { ?item  wdt:P569  ?DR }'
        + 'OPTIONAL { ?item  wdt:P570  ?RIP }'
        + 'OPTIONAL { ?item  wdt:P18  ?image }'
        + 'SERVICE wikibase:label { bd:serviceParam wikibase:language  "en"}}'
        + 'GROUP BY ?item ?itemLabel ?itemDescription';

    var queryUrl = encodeURI(url + '?query=' + query + '&format=json');

    fetch(queryUrl)
        .then(response => response.json())        
        .then(data => data.results.bindings[0])
        .then(data => console.table(data));
}
