var XmlHttp = createXmlHttpRequestObject();
//
function createXmlHttpRequestObject() {
    var XmlHttp;
    if (window.ActiveXObject) {
        //window.ActiveXObject:判断是否支持ActiveX控件xmlhttp
        try {
            XmlHttp = new ActiveXObject("Microsoft.XMLHTTP");
        } catch (e) {
            //e:
            XmlHttp = false;
        }
    } else {
        try{
            XmlHttp=new XMLHttpRequest();
        } catch (e) {
            XmlHttp = false;
        }
    }
    if (!XmlHttp)
        alert("can not create");
    else
        return XmlHttp;
}

// 
function process() {
    if(XmlHttp.readyState==0||XmlHttp.readyState==4){
            //readyState
        food = encodeURIComponent(document.getElementById("userInput").Value);
            //getElementById:
            //getElementsByClassName:
            //getElementByName:
            //getElementByTagName:
            //getElementByTagNameNS:
        XmlHttp.open("GET", "FoodStore.php?food=" + food, true);
        XmlHttp.onreadystatechange = handleServerResponse;
        XmlHttp.send(null);
        } else {
        setTimeout('process()', 1000);
    }
}

//
function handleServerResponse() {
    if (XmlHttp.readyState == 4) {
        if (XmlHttp.status == 200) {
            xmlResponse = XmlHttp.responseXML;
            xmlDocumentElement = xmlResponse.documentElement;
            message = xmldocumentElement.firstChild.data;
            document.getElementById("underInput").innerHTML = '<span style="color:red">'+message+'</span>';
            setTimeout('process()', 1000);
        } else {
            alert('wrong');
        }
        }
    }