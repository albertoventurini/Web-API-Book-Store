$(function() {

    loadBooks();

    // selettore

    $('#searchFilter').change(function() {
        var query = $(this).val();
        loadBooks(query);
    });

    //var loginData = {
    //    username: 'alberto@codiceplastico.com',
    //    password: 'password'
    //};

    //var encodedLoginData = btoa(loginData.username + ':' + loginData.password);

    //sessionStorage.setItem('authenticationHeader', encodedLoginData);



    function loadBooks(query) {
        $('#bookList').empty();
        var url = "/api/books";
        if (query) {
            url = url + "?query=" + query;
        }

        //var headers = {};
        //var encodedLoginData = sessionStorage.getItem('authenticationHeader');
        //if (encodedLoginData) {
        //    headers.Authorization = 'Basic ' + encodedLoginData;
        //}

        myAjax({
            type: 'GET',
            url: url
        }).done(function(data) {
            data.forEach(function(book) {
                $('#bookList').append(
                    '<li>' + book.Author.Name + ' - ' + book.Title
                    + '<button class="btnAddToBasket" data-book="' + book.Id + '">Aggiungi al carrello</button>'
                    + '</li>'
                );
            });

            $('.btnAddToBasket').on('click', function() {

                var bookId = Number($(this).attr('data-book'));
                myAjax({
                    type: 'PUT',
                    url: '/api/baskets/book/',
                    contentType: 'application/json',
                    data: JSON.stringify({
                        'bookId': bookId
                    })
                }).done(function (data) {


                }).fail(function (data) {
                    alert(data);
                });
            });
        });
    }

    $('#btnLogin').on('click', function() {
        var username = $('#username').val();
        var password = $('#password').val();

        var credentials = username + ':' + password;
        var encodedCredentials = btoa(credentials);

        sessionStorage.setItem('credentials', encodedCredentials);
        sessionStorage.setItem('username', username);
    });

    $('#showBasket').on('click', function() {
        myAjax({
            type: 'GET',
            url: '/api/baskets/' + sessionStorage.getItem('username')
        }).done(function (data) {
            $('#basket').empty();
            data.Books.forEach(function(book) {
                $('#basket').append(
                    '<li>' + book.Title +  '</li>'
                );
            });
        }).fail(function(data) {
            alert(data);
        });
    });

    function myAjax(parameters) {
        var credentials = sessionStorage.getItem('credentials');
        parameters.headers = parameters.headers || {};
        if (credentials) {
            parameters.headers.Authorization = 'Basic ' + credentials;
        }

        return $.ajax(parameters);
    }

});