// This is a simple *viewmodel* - JavaScript that defines the data and behavior of your UI
function AppViewModel() {
    this.id = ko.observable("303");
    this.name = ko.observable("Elvis Presley");
    this.description = ko.observable("American singer and actor");
    this.birth = ko.observable("1935");
    this.death = ko.observable("1977");
    this.image = ko.observable("http://commons.wikimedia.org/wiki/Special:FilePath/PresleyPromo1954PhotoOnly.jpg");
    this.link = ko.observable("https://en.wikipedia.org/wiki/Elvis_Presley");
    this.rating = ko.observable("7");


    //this.fullName = ko.computed(function () {
    //    return this.firstName() + " " + this.lastName();
    //}, this);

    //this.capitalizeLastName = function () {
    //    var currentVal = this.lastName();        // Read the current value
    //    this.lastName(currentVal.toUpperCase());
    //};
};

// Activates knockout.js
ko.applyBindings(new AppViewModel());

