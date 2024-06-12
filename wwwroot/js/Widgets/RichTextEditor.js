(function (pageBuilder) {
    var richTextEditor = pageBuilder.richTextEditor = pageBuilder.richTextEditor || {};
    var configurations = richTextEditor.configurations = richTextEditor.configurations || {};
    // Overrides the "default" configuration of the Rich text widget, assuming you have not specified a different configuration
    // The below configuration adds the h5 and h6 values to the paragraphFormat and removes the code value. Also sets the toolbar to be visible without selecting any text.
    configurations["default"] = {
        toolbarVisibleWithoutSelection: true,
        inlineClasses: {
           'TitoloParagrafo': 'Titolo oro',
           'TitoloParagrafoSmall': 'Titolo',
           'TextHighlight': 'Evidenziazione',
           'pBig': 'Testo paragrafo grande',
           'TextBoldGold': 'Grassetto oro',
           'linkDiscoverMore': 'Link Discover More',
           'linkDiscoverMoreBlack': 'Link Discover More Black',
           'linkDiscoverMoreWhite': 'Link Discover More White',
           'linkDownload': 'Link Download',
           'linkSempliceOro': 'Link semplice oro',
           'titleNewsletter': 'Titolo newsletter'
        },
        colorsText: [
            '#61BD6D', '#1ABC9C', 'REMOVE'
        ]
    };

    // Defines a new configuration for a simple toolbar with only formatting
    // options and disables the inline design of the toolbar
    configurations["simple"] = {
        toolbarButtons: ['paragraphFormat', '|', 'bold', 'italic', 'underline', '|', 'align', 'formatOL', 'formatUL'],
        paragraphFormatSelection: true,
        toolbarInline: false
    };
})(window.kentico.pageBuilder);