function Form() {

    var context;
    this.dropdown = $('.chosen-select');

    this.init = function () {
        this.createDropdown();
    }

    this.createDropdown = function () {
        this.dropdown.select2({
            minimumResultsForSearch: Infinity,
            allowClear: false
        }).on('select2:unselecting', function () {
            $(this).data('unselecting', true);
        }).on('select2:opening', function (e) {
            if ($(this).data('unselecting')) {
                $(this).removeData('unselecting');
                e.preventDefault();
            }
        });
    }

    context = this;
    this.init();

}
$(document).ready(function () {
    var form = new Form();
});