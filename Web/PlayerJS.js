var book = document.getElementById("book");
var audio = document.getElementById("myAudio");
var controlPanel = document.getElementById("controlPanel");
var btnArea = document.getElementById("btnArea");
var info = document.getElementById("info");
var setVolValue = document.getElementById("setVolValue");
var volValue = document.getElementById("volValue");
var progress = document.getElementById("progress");
var music = document.getElementById("music");

console.log(btnArea.children[6]);

let btnPlay = btnArea.children[0];
let btnMuted = btnArea.children[6];


function showBook() {
    book.className = book.className == "" ? "hide" : "";
}

var option;
for (var i = 0; i < book.children[0].children.length; i++) {
    book.children[0].children[i].draggable = "true";
    book.children[0].children[i].id = "song" + i;

    //為歌曲加入ondragstart事件
    book.children[0].children[i].ondragstart = drag;

    option = document.createElement("option");
    //<option value="歌的路徑">歌名</option>

    option.value = book.children[0].children[i].title;
    option.innerText = book.children[0].children[i].innerText;
    music.appendChild(option);
}
changeMusic(0);


function updateMusic() {
    //1. 把music原先的option全部移除
    //for (var i = music.children.length-1; i >=0 ; i--) {
    //    music.removeChild(music.children[i]);
    //}

    for (var i = music.children.length - 1; i >= 0; i--) {
        music.remove(i);
    }

    //2. 再讀取我的歌本內的歌曲,append至music下拉選單
    for (var i = 0; i < book.children[1].children.length; i++) {

        option = document.createElement("option");
        //<option value="歌的路徑">歌名</option>

        option.value = book.children[1].children[i].title;
        option.innerText = book.children[1].children[i].innerText;
        music.appendChild(option);
    }

    changeMusic(0);
}


//上一首,下一首
//function prevNextMusic(n) {
//    console.log(music.selectedIndex+n);

//    changeMusic(n)

//}

//當來源物件被拖曳到目標區上方時呼叫他
function allowDrop(ev) {
    ev.preventDefault();  //停止物件預設行為
}


//當來源物件被拖曳時呼叫它
function drag(ev) {
    ev.dataTransfer.setData("aaa", ev.target.id);
}

//當來源物件被丟到目標區時呼叫他
function drop(ev) {
    ev.preventDefault();
    var data = ev.dataTransfer.getData("aaa");

    if (ev.target.id == "")
        ev.target.appendChild(document.getElementById(data));
    else
        ev.target.parentNode.appendChild(document.getElementById(data));
    //book.children[1].appendChild(document.getElementById(data));
}

//全曲循環
function setAllLoop() {
    info.children[2].innerText = info.children[2].innerText == "全曲循環" ? "正常" : "全曲循環";
}


//隨機播放
function setRandom() {
    info.children[2].innerText = info.children[2].innerText == "隨機播放" ? "正常" : "隨機播放";
}


//單曲循環
function setLoop() {
    info.children[2].innerText = info.children[2].innerText == "單曲循環" ? "正常" : "單曲循環";
}


//選擇歌曲
function changeMusic(i) {
    //console.log(music.selectedIndex + i);

    audio.children[0].src = music.options[music.selectedIndex + i].value;
    audio.children[0].title = music.options[music.selectedIndex + i].innerText;
    music.options[music.selectedIndex + i].selected = true;
    audio.load();

    if (btnPlay.innerText == ";")
        playAudio();
}


//播放進度拖曳
function setProgressBar() {
    audio.currentTime = progress.value;
}


//將時間格式從秒轉為分與秒
var min = 0, sec = 0;
function getTimeFormat(timeSec) {
    //if...else...三元表達式
    min = parseInt(timeSec / 60);
    sec = parseInt(timeSec % 60);
    min = min < 10 ? "0" + min : min;
    sec = sec < 10 ? "0" + sec : sec;

    return min + ":" + sec;
}

//取得目前播放時間
var w = 0;
var r = 0; //這個變數是存放隨機播放的亂數值
getDuration();
function getDuration() {
    info.children[1].innerText = getTimeFormat(audio.currentTime) + " / " + getTimeFormat(audio.duration);

    w = audio.currentTime / audio.duration * 100;
    console.log(w);
    progress.style.backgroundImage = "-webkit-linear-gradient(left,#b40000,#b40000 " + w + "%, #a5ffff " + w + "% ,#a5ffff)";
    progress.max = parseInt(audio.duration);
    progress.value = parseInt(audio.currentTime);


    //當一首歌播完時,我要自動換下一首歌
    if (audio.currentTime == audio.duration) {

        console.log(info.children[2].innerText == "單曲循環");
        if (info.children[2].innerText == "單曲循環")
            changeMusic(0);
        else if (info.children[2].innerText == "隨機播放") {
            //在歌曲池子裡隨機抓取一首歌
            r = Math.floor(Math.random() * music.options.length);    //0~歌曲最終索引值(目前r=0~4)
            console.log("r=" + r); //假設0
            r = r - music.selectedIndex;  //假設目前歌曲索引值=2 , 0-2得到-2

            changeMusic(r);
        }
        //else if (info.children[2].innerText == "全曲循環" && music.selectedIndex == music.options.length - 1)
        //    stopAudio();
        else if (music.selectedIndex == music.options.length - 1) {
            if (info.children[2].innerText == "全曲循環")
                changeMusic(-music.selectedIndex);
            else
                stopAudio();
        }

        else
            changeMusic(1);
    }



    //if (audio.currentTime < audio.duration)
    setTimeout(getDuration, 50);
    //else {
    //如果目前不是最後一首,前進下一首歌
    //}
}

setVolume();
//音量調整
function setVolume() {
    volValue.value = setVolValue.value;
    audio.volume = setVolValue.value / 100;

    setVolValue.style.backgroundImage = "-webkit-linear-gradient(left,#ef7d2c,#ef7d2c " + setVolValue.value + "%, #c8c8c8 " + setVolValue.value + "% ,#c8c8c8)";
}

//音量微調
function btnSetVolume(vol) {
    setVolValue.value = parseInt(volValue.value) + vol;
    setVolume();
}


//設定靜音
function setMuted() {
    audio.muted = !audio.muted;
    btnMuted.style.textDecoration = audio.muted ? "line-through" : "none";

}


//快轉及倒轉
function changeTime(sec) {
    audio.currentTime += sec;
}


//音樂播放
function playAudio() {
    audio.play();
    btnPlay.innerText = ";";
    btnPlay.onclick = pauseAudio;
    info.children[0].innerText = "現正播放:" + audio.children[0].title;

}

//音樂暫停
function pauseAudio() {
    audio.pause();
    btnPlay.innerText = "4";
    btnPlay.onclick = playAudio;
    info.children[0].innerText = "音樂暫停";
}

//音樂停止
function stopAudio() {
    pauseAudio();
    audio.currentTime = 0;
    info.children[0].innerText = "音樂停止";
}