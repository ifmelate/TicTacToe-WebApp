TicTacToe.Views.Game = {
    Game: null,
    Options: null,
    Initialized: false,
    Init: function(options) {
        Game = this;
        this.Options = options;

        $('#gameForm').on('click', '.col-4', function (e) {
            var cellId = $(this).find("#cell_CellId").val();
            var gameId = $(this).find("#cell_GameId").val();
            $.ajax({
                type: 'POST',
                url: Game.Options.MakePlayerMoveUrl,
                async:false,
                data: {
                    gameId: gameId,
                    cellId: cellId
                },
                traditional: true,
                success: function (data) {
                    $("#gameForm").html('');
                    $("#gameForm").html(data);
                },
                complete: function () {
                    Game.UpdateGameInfo(gameId);
                }
            });

            setTimeout(
                function () {
                    $.ajax({
                        type: 'POST',
                        url: Game.Options.MakeComputerMoveUrl,
                        async: false,
                        data: {
                            gameId: gameId
                        },
                        traditional: true,
                        success: function (data) {
                            $("#gameForm").html('');
                            $("#gameForm").html(data);
                        },
                        complete: function () {
                            Game.UpdateGameInfo(gameId);
                        }
                    });
                }, 500);
           
        });

        this.Initialized = true;
    },
    UpdateGameInfo: function(gameId) {
        $.ajax({
            type: 'POST',
            url: Game.Options.UpdateGameInfoUrl,
            traditional: true,
            data: {
                gameId: gameId
            },
            success: function (data) {
                $("#gameInfoForm").html(data);
            },
            complete: function () {

            }
        });
    }
};