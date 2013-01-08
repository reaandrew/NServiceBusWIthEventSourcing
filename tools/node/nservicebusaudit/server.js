var http = require('http');
var Table = require('cli-table');


var options = {
    host: '192.168.1.5',
    path: '/Contact.WebApi/api/audit',
    headers: {'Accept': 'application/json'}
};

callback = function(response) {
    var str = '';

    response.on('data', function (chunk) {
        str += chunk;
    });

    response.on('end', function () {
        var table = new Table({
            head: ['From','Message Type','Avg','Min','Max']
            , colWidths: [35, 35, 15,15,15]
        });
        var data = JSON.parse(str);
        for(var item in data){
            dataItem = data[item];
            table.push([dataItem.OriginatingAddress,dataItem.MessageType,dataItem.AverageProcessingTime,dataItem.MinProcessingTime,dataItem.MaxProcessingTime]);
        }
        console.log(table.toString());
    });
}

http.request(options, callback).end();
