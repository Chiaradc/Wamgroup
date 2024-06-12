(function (component) {
    var pieArray = [];
    var textArea = null;
    var editorDiv = null;

    component.init = function (id) {
        textArea = $('[data-pie-editor-control-id="' + id + '"]');
        editorDiv = $('[data-pie-editor-div-id="' + id + '"]');
        pieArray = JSON.parse(textArea.val());
        redraw();
    }

    function redraw() {
        var finalHtml = '';
        for (var i = 0; i < pieArray.length; i++) {
            var pieValue = pieArray[i];
            finalHtml +=
                '<div class="el-pie-container el-icons">' +
                    '<div>' + (i + 1) + '</div>' +
                    '<div><a href="#" data-delete="' + i + '"><i class="icon-bin"></i></a></div>' +
                    '<div>Testo: <input data-ktc-notobserved-element type="text" value="' + pieValue.name+'" data-text="' + i + '"/></div>' +
                    '<div>Percentuale: <input  data-ktc-notobserved-element type="text" value="' + pieValue.value +'" data-perc="' + i + '" /></div>' +
                '</div>';
        }
        finalHtml += '<div class="el-tab-container el-icons">' +
            '<div></div>' +
            '<div></div>' +
            '<div><a href="#" data-add><i class="icon-plus-circle"></i></a></div>'
        '</div>';

        editorDiv.html(finalHtml);

        editorDiv.find('[data-text]').each(function () {
            var self = $(this);
            self.on('change', function () {
                var rowNum = parseInt(self.attr('data-text'), 10);
                pieArray[rowNum].name = self.val();
                textArea.val(JSON.stringify(pieArray));
                redraw();
            });
        });

        editorDiv.find('[data-perc]').each(function () {
            var self = $(this);
            self.on('change', function () {
                var rowNum = parseInt(self.attr('data-perc'), 10);
                pieArray[rowNum].value = parseInt(self.val(), 10);
                textArea.val(JSON.stringify(pieArray));
                redraw();
            });
        });

        editorDiv.find('a[data-add]').each(function () {
            $(this).on('click', function () {
                addPie();
            });
        });

        editorDiv.find('a[data-delete]').each(function () {
            var self = $(this);
            $(this).on('click', function () {
                var rowNum = parseInt(self.attr('data-delete'), 10);
                if (confirm('Si vuole eliminare i valori in posizione ' + (rowNum + 1) + '. Procedere?'))
                    deleteTab(rowNum);
            })
        });
    }

    function addPie() {
        pieArray.push({
            'name': '',
            'value': 0
        });
        redraw();
    }

    function deleteTab(rowNumber) {
        pieArray.splice(rowNumber, 1);
        textArea.val(JSON.stringify(pieArray));
        redraw();
    }

}(window.$eloPieEditor = window.$eloPieEditor || {}));