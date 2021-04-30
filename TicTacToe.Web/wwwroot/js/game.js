TicTacToe.Views.Game = {
    Game: null,
    Options: null,
    Initialized: false,
    Init: function(options) {
        Game = this;
        this.Options = options;

        $('#gameForm .col-4').click(function () {
            var cellId = $(this).find("#cell_CellId").val();
            var gameId = $(this).find("#cell_GameId").val();
            $.ajax({
                type: 'POST',
                url: Game.Options.ReloadGameCellsUrl,
                data: {
                    gameId: gameId,
                    cellId: cellId
                },
                traditional: true,
                success: function (data) {

                },
                complete: function () {

                }
            });
        });

        this.Initialized = true;
    }
};