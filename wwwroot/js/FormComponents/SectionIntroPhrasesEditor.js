(function (component) {
    var phrases = [];
    var textArea = null;
    var editorDiv = null;

    component.init = function (id) {
        textArea = $('[data-section-intro-phrases-editor-control-id="' + id + '"]');
        editorDiv = $('[data-section-intro-phrases-editor-div-id="' + id + '"]');
        phrases = JSON.parse(textArea.val());
        redraw();
    }

    function redraw() {
        var finalHtml = '';
        for (var i = 0; i < phrases.length; i++) {
            var phrase = phrases[i];
            finalHtml += '<div class="el-column-container el-icons">' +
                '<div>' + (i + 1) + '</div>' +
                '<div><a href="#" data-delete="' + i + '"><i class="icon-bin"></i></a></div>' +                
                '<div data-row="' + i +'"><a href="#" data-settings="' + i + '"><i class="icon-cogwheel"></i></a>' +                
                    '<div class="gg-phrase-panel">' +
                        '<div class="gg-prhase-panel-header"><a href="#" data-close><i class="icon-modal-close"></i></a></div>'+
                        '<div>'+
                            '<div><strong>Allineamento</strong></div>'+
                            '<div><input type="radio" data-ktc-notobserved-element id="gg-phrase-indent-left-L1-'+i+'" name="gg-phrase-indent-'+i+'" value="LeftPlusPlus" /><label for="gg-phrase-indent-left-L1-'+i+'">sinistro ++</label></div>' +
                            '<div><input type="radio" data-ktc-notobserved-element id="gg-phrase-indent-left-L2-'+i+'" name="gg-phrase-indent-'+i+'" value="LeftPlus" /><label for="gg-phrase-indent-left-L2-'+i+'">sinistro +</label></div>' +
                            '<div><input type="radio" data-ktc-notobserved-element id="gg-phrase-indent-center-'+i+'" name="gg-phrase-indent-'+i+'" value="C" /><label for="gg-phrase-indent-center-'+i+'">centrato</label></div>' +
                            '<div><input type="radio" data-ktc-notobserved-element id="gg-phrase-indent-left-R1-'+i+'" name="gg-phrase-indent-'+i+'" value="RightPlus" /><label for="gg-phrase-indent-left-R1-'+i+'">destro +</label></div>' +
                            '<div><input type="radio" data-ktc-notobserved-element id="gg-phrase-indent-left-R2-'+i+'" name="gg-phrase-indent-'+i+'" value="RightPlusPlus" /><label for="gg-phrase-indent-left-R2-'+i+'">destro ++</label></div>' +
                            '<div><strong>Posizione</strong></div>' +
                            '<div><input type="checkbox" data-ktc-notobserved-element id="gg-phrase-below-'+i+'" /><label for="gg-phrase-below-'+i+'">&nbsp;sotto l\'immagine</label></div>' +
                        '</div>'+
                    '</div>'+
                '</div>' +
                '<div class="el-column gg-phrase">' +
                '<div data-row="' + i +'"><a href="#" data-font="' + i + '"><i class="icon-a-lowercase"></i></a>' +
                '<div class="gg-phrase-panel">' +
                        '<div class="gg-prhase-panel-header"><a href="#" data-close><i class="icon-modal-close"></i></a></div>'+
                        '<div>'+
                            '<div><strong>Font</strong></div>'+
                            '<div><input type="checkbox" data-ktc-notobserved-element data-id="gg-phrase1-serif" id="gg-phrase1-serif-'+i+'" /><label for="gg-phrase1-serif-'+i+'">&nbsp;font con grazie</label></div>' +
                            '<div><input type="checkbox" data-ktc-notobserved-element data-id="gg-phrase1-bold" id="gg-phrase1-bold-'+i+'" /><label for="gg-phrase1-bold-'+i+'">&nbsp;grassetto</label></div>' +
                            '<div><input type="checkbox" data-ktc-notobserved-element data-id="gg-phrase1-italic" id="gg-phrase1-italic-'+i+'" /><label for="gg-phrase1-italic-'+i+'">&nbsp;corsivo</label></div>' +
                        '</div>'+
                    '</div>'+
                '</div>' +
                '<input type="text" data-ktc-notobserved-element data-phrase="1" data-change-data="' + i + '" value="' + phrase.Phrase1 + '" />' +
                '</div>' +
                '<div class="el-column gg-phrase">' +
                '<div data-row="' + i +'"><a href="#" data-font="' + i + '"><i class="icon-a-lowercase"></i></a>' +
                '<div class="gg-phrase-panel" data-font="' + i +'">' +
                        '<div class="gg-prhase-panel-header"><a href="#" data-close><i class="icon-modal-close"></i></a></div>'+
                        '<div>'+
                            '<div><strong>Font</strong></div>' +
                            '<div><input type="checkbox" data-ktc-notobserved-element data-id="gg-phrase2-serif" id="gg-phrase2-serif-'+i+'" /><label for="gg-phrase2-serif-'+i+'">&nbsp;font con grazie</label></div>' +
                            '<div><input type="checkbox" data-ktc-notobserved-element data-id="gg-phrase2-bold" id="gg-phrase2-bold-'+i+'" /><label for="gg-phrase2-bold-'+i+'">&nbsp;grassetto</label></div>' +
                            '<div><input type="checkbox" data-ktc-notobserved-element data-id="gg-phrase2-italic" id="gg-phrase2-italic-'+i+'" /><label for="gg-phrase2-italic-'+i+'">&nbsp;corsivo</label></div>' +
                        '</div>'+
                    '</div>'+
                '</div>' +
                '<input type="text" data-ktc-notobserved-element data-phrase="2" data-change-data="' + i + '" value="' + phrase.Phrase2 + '" />' +                
                '</div>' +
                '</div>' +
                '</div>';
        }
        finalHtml += '<div class="el-column-container el-icons">' +
            '<div></div>' +
            '<div></div>' +
            '<div><a href="#" data-add><i class="icon-plus-circle"></i></a></div>'
        '</div>';

        editorDiv.html(finalHtml);

        editorDiv.find('a[data-add]').each(function () {
            $(this).on('click', function () {
                addPhrase();
            });
        });

        editorDiv.find('a[data-delete]').each(function () {
            var self = $(this);
            $(this).on('click', function () {
                var rowNum = parseInt(self.attr('data-delete'), 10);
                if (confirm('Si vuole eliminare la frase ' + (rowNum + 1) + '. Procedere?'))
                    deletePhrase(rowNum);
            })
        });

        editorDiv.find('[data-change-data]').each(function () {
            $(this).on('change', function () {                
                var rowNumber = parseInt($(this).attr('data-change-data'), 10);
                phrases.forEach((element, index) => {
                    if (rowNumber == index) {
                        switch ($(this).attr('data-phrase')) {
                            case '1':
                                element.Phrase1 = $(this).val();
                                break;
                            case '2':
                                element.Phrase2 = $(this).val();
                                break;
                        }
                    }
                });
                textArea.val(JSON.stringify(phrases));
            });
        });

        editorDiv.find('a[data-settings]').each(function () {
            var container = $(this).parent();
            container.find('a[data-close]').on('click', function () {
                container.find('.gg-phrase-panel').removeClass('active');
            });
            $(this).on('click', function () {
                container.find('.gg-phrase-panel').addClass('active');
                initSettings(container, parseInt(container.attr('data-row'), 10));
            })
            container.find('[name^="gg-phrase-indent"]').each(function () {
                var self = $(this);
                $(this).on('click', function () {
                    setIndent(self, parseInt(container.attr('data-row'), 10));
                })
            });
            container.find('[id^="gg-phrase-below"]').each(function () {
                var self = $(this);
                $(this).on('click', function () {
                    setBelow(self, parseInt(container.attr('data-row'), 10));
                })
            });
        });

        editorDiv.find('a[data-font]').each(function () {
            var container = $(this).parent();
            container.find('a[data-close]').on('click', function () {
                container.find('.gg-phrase-panel').removeClass('active');
            });
            $(this).on('click', function () {
                container.find('.gg-phrase-panel').addClass('active');
                initFont(container, parseInt(container.attr('data-row'), 10));
            });
            container.find('[type="checkbox"]').each(function () {
                var self = $(this);
                $(this).on('click', function () {
                    setFont(self, parseInt(container.attr('data-row'), 10));
                })
            });
        });
    }

    function addPhrase() {
        phrases.push({
            'Indent': 'C',
            'Below': false,
            'Phrase1': '',
            'FontSerifPhrase1': false,
            'FontBoldPhrase1': false,
            'FontItalicPhrase1': false,
            'Phrase2': '',
            'FontSerifPhrase2': false,
            'FontBoldPhrase2': false,
            'FontItalicPhrase2': false
        });
        redraw();
    }

    function deletePhrase(rowNumber) {
        phrases.splice(rowNumber, 1);
        textArea.val(JSON.stringify(phrases));
        redraw();
    }

    function initSettings(container, rowNumber) {
        phrases.forEach((element, index) => {
            if (rowNumber == index) {
                container.find('[name^="gg-phrase-indent"][value="'+element.Indent+'"]').prop('checked', true);
                container.find('[id^="gg-phrase-below"]').prop('checked', element.Below);
            }
        });
    }

    function initFont(container, rowNumber) {
        phrases.forEach((element, index) => {
            if (rowNumber == index) {
                container.find('[type="checkbox"]').each(function () {
                    switch ($(this).attr('data-id')) {
                        case 'gg-phrase1-serif':
                            $(this).prop('checked', element.FontSerifPhrase1);
                            break;
                        case 'gg-phrase1-bold':
                            $(this).prop('checked', element.FontBoldPhrase1);
                            break;
                        case 'gg-phrase1-italic':
                            $(this).prop('checked', element.FontItalicPhrase1);
                            break;
                        case 'gg-phrase2-serif':
                            $(this).prop('checked', element.FontSerifPhrase2);
                            break;
                        case 'gg-phrase2-bold':
                            $(this).prop('checked', element.FontBoldPhrase2);
                            break;
                        case 'gg-phrase2-italic':
                            $(this).prop('checked', element.FontItalicPhrase2);
                            break;
                    }
                });
            }
        });
    }

    function setIndent(inputCtrl, rowNumber) {
        //effettuo il cambio
        phrases.forEach((element, index) => {
            if (rowNumber == index) {
                element.Indent = inputCtrl.val();
            }
        });
        textArea.val(JSON.stringify(phrases));
    }

    function setBelow(inputCtrl, rowNumber) {
        //effettuo il cambio
        phrases.forEach((element, index) => {
            if (rowNumber == index) {
                element.Below = inputCtrl.is(':checked');
            }
        });
        textArea.val(JSON.stringify(phrases));
    }

    function setFont(inputCtrl, rowNumber) {
        //effettuo il cambio
        phrases.forEach((element, index) => {
            if (rowNumber == index) {
                switch (inputCtrl.attr('data-id')) {
                    case 'gg-phrase1-serif':
                        element.FontSerifPhrase1 = inputCtrl.is(':checked');
                        break;
                    case 'gg-phrase1-bold':
                        element.FontBoldPhrase1 = inputCtrl.is(':checked');
                        break;
                    case 'gg-phrase1-italic':
                        element.FontItalicPhrase1 = inputCtrl.is(':checked');
                        break;
                    case 'gg-phrase2-serif':
                        element.FontSerifPhrase2 = inputCtrl.is(':checked');
                        break;
                    case 'gg-phrase2-bold':
                        element.FontBoldPhrase2 = inputCtrl.is(':checked');
                        break;
                    case 'gg-phrase2-italic':
                        element.FontItalicPhrase2 = inputCtrl.is(':checked');
                        break;
                }
            }
        });
        textArea.val(JSON.stringify(phrases));
    }

}(window.$eloSectionIntroPhrasesEditor = window.$eloSectionIntroPhrasesEditor || {}));