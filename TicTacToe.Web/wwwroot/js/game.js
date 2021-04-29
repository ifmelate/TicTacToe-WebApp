TicTacToe.Views.Game = {
    Game: null,
    Options: null,
    Initialized: false,
    Init: function(options) {
        Game = this;
        this.Options = options;
        $('#gameForm .col-4').click(function () {
            $(this).find('.empty-cell').addClass('hidden');
            $(this).find('.cross-cell').removeClass('hidden');
        });
        this.Initialized = true;
    }
};