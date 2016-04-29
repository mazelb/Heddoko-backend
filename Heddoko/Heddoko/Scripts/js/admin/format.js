var Format = {
    materialsType: {
        Name: {
            maxLength: 50
        }
    },
    image: function (url) {
        return url ? '<img class="img-grid" src="' + url + '" />' : '&nbsp;';
    },
};