﻿// This is a simple *viewmodel* - JavaScript that defines the data and behavior of your UI
function AppViewModel() {
    this.id = ko.observable("303");
    this.name = ko.observable("Elvis Presley");
    this.description = ko.observable("American singer and actor");
    this.birth = ko.observable("1935");
    this.death = ko.observable("1977");
    this.image = ko.observable("http://commons.wikimedia.org/wiki/Special:FilePath/PresleyPromo1954PhotoOnly.jpg");
    this.link = ko.observable("https://en.wikipedia.org/wiki/Elvis_Presley");
    this.rating = ko.observable("7");


    this.life = ko.computed(function () {
        return this.birth() + " - " + this.death();
    }, this);

    //this.capitalizeLastName = function () {
    //    var currentVal = this.lastName();        // Read the current value
    //    this.lastName(currentVal.toUpperCase());
    //};
}

// Activates knockout.js
ko.applyBindings(new AppViewModel());

var id = 'Q7747';
//GetWikiPerson(id);

function GetWikiPerson(qid) {
    var url = "https://query.wikidata.org/sparql";
    var personId = "http://www.wikidata.org/entity/" + qid;

    var query = 'SELECT DISTINCT  ?item ?itemLabel ?itemDescription (SAMPLE(?DR) AS ?DRSample) (SAMPLE(?article) AS ?articleSample)'
        + 'WHERE{ ?article  schema:about       ?item ; schema:inLanguage  "en" ; schema:isPartOf    <https://en.wikipedia.org/>'
        + 'FILTER ( ?item = <"' + personId + '">'
        + 'OPTIONAL { ?item  wdt:P569  ?DR }'
        + 'OPTIONAL { ?item  wdt:P570  ?RIP }'
        + 'OPTIONAL { ?item  wdt:P18  ?image }'
        + 'SERVICE wikibase:label { bd:serviceParam wikibase:language  "en"}}'
        + 'GROUP BY ?item ?itemLabel ?itemDescription';

    var queryUrl = encodeURI(url + '?query=' + query + '&format=json');

    fetch(queryUrl)
        .then(response => response.json())
        .then(data => data.results.bindings[0])
        .then(data => console.log(data));

}

