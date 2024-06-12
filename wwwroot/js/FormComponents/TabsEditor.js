(function (component) {
    var tabsArray = [];
    var textArea = null;
    var editorDiv = null;
    var emptyImage = 'data:image/jpeg;base64,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';

    component.init = function (id) {
        textArea = $('[data-tabs-editor-control-id="' + id + '"]');
        editorDiv = $('[data-tabs-editor-div-id="' + id + '"]');
        tabsArray = JSON.parse(textArea.val());
        redraw();
    }

    function redraw() {
        var finalHtml = '';
        for (var i = 0; i < tabsArray.length; i++) {
            var tab = tabsArray[i];
            finalHtml +=
                '<div class="el-tab-container el-icons">' +
                    '<div>' + (i + 1) + '</div>' +
                    '<div><a href="#" data-delete="' + i + '"><i class="icon-bin"></i></a></div>' +
                    '<div><a href="" data-image-type="img" data-index="' + i + '"><i class="icon-plus-circle"></i></a><i class="icon-picture"></i><img data-image-tablet src="'+(tab.Image != '' ? tab.Image : emptyImage)+'" /></div>' +
                    '<div><a href="" data-image-type="img-hover" data-index="' + i + '"><i class="icon-plus-circle"></i></a><i class="icon-picture hover"></i><img data-image-tablet src="' + (tab.HoverImage != '' ? tab.HoverImage : emptyImage) +'" /></div>' +                    
                '</div>' +
                '<div class="el-tab-container el-icons">' +
                '<div>Testo: <input data-ktc-notobserved-element type="text" value="' + tab.Text+'" data-text="' + i + '"/></div>' +
                '<div>Alt immagine: <input  data-ktc-notobserved-element type="text" value="' + tab.ImageAlt +'" data-alt="' + i + '" /></div>' +
                '</div>';
        }
        finalHtml += '<div class="el-tab-container el-icons">' +
            '<div></div>' +
            '<div></div>' +
            '<div><a href="#" data-add><i class="icon-plus-circle"></i></a></div>'
        '</div>';

        editorDiv.html(finalHtml);

        editorDiv.find('a[data-image-type]').each(function () {
            var self = $(this);
            $(this).on('click', function (e) {
                e.preventDefault();
                e.stopPropagation();
                var dialogOptions = {
                    tabs: ["media"],


                    // Defines the applyCallback function invoked on click of the selector's confirmation button
                    applyCallback: function (data) {
                        items = data.items;
                        if (items && items.length) {
                            // 'newItem' is a media file object
                            var newItem = items[0];
                            var rowNum = parseInt(self.attr('data-index'), 10);
                            if (self.is('[data-image-type="img"]'))
                                tabsArray[rowNum].Image = newItem.liveSiteUrl;
                            else
                                tabsArray[rowNum].HoverImage = newItem.liveSiteUrl;
                            textArea.val(JSON.stringify(tabsArray));
                            redraw();

                            return {
                                closeDialog: true
                            };
                        }
                    }
                };
                window.kentico.modalDialog.contentSelector.open(dialogOptions);
            });
        });

        editorDiv.find('[data-text]').each(function () {
            var self = $(this);
            self.on('change', function () {
                var rowNum = parseInt(self.attr('data-text'), 10);
                tabsArray[rowNum].Text = self.val();
                textArea.val(JSON.stringify(tabsArray));
                redraw();
            });
        });

        editorDiv.find('[data-alt]').each(function () {
            var self = $(this);
            self.on('change', function () {
                var rowNum = parseInt(self.attr('data-alt'), 10);
                tabsArray[rowNum].ImageAlt = self.val();
                textArea.val(JSON.stringify(tabsArray));
                redraw();
            });
        });

        editorDiv.find('a[data-add]').each(function () {
            $(this).on('click', function () {
                addTab();
            });
        });

        editorDiv.find('a[data-delete]').each(function () {
            var self = $(this);
            $(this).on('click', function () {
                var rowNum = parseInt(self.attr('data-delete'), 10);
                if (confirm('Si vuole eliminare la tab ' + (rowNum + 1) + '. Procedere?'))
                    deleteTab(rowNum);
            })
        });
    }

    function addTab() {
        tabsArray.push({
            'Text': '',
            'Image': '',
            'HoverImage': '',
            'ImageAlt': ''
        });
        redraw();
    }

    function deleteTab(rowNumber) {
        tabsArray.splice(rowNumber, 1);
        textArea.val(JSON.stringify(tabsArray));
        redraw();
    }

}(window.$eloTabsEditor = window.$eloTabsEditor || {}));