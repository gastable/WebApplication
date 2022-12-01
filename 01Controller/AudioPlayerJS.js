var btnArea = document.getElementById("btnArea");
var audio = document.getElementById("myAudio");
var volValue = document.getElementById("volValue");
var info = document.getElementById("info");
var setVolValue = document.getElementById("setVolValue");
var progress = document.getElementById("progress");
var music = document.getElementById("music");
var songList = document.getElementById("songList");
let btnPlay = btnArea.children[0];
let btnMuted = btnArea.children[9];

volValue.value = setVolValue.value;
audio.volume = setVolValue.value / 100;

var book = document.getElementById("book");
var option;
for (i = 0; i < book.children[0].children.length; i++) {//用迴圈自動寫入屬性
    book.children[0].children[i].id = "song" + i;
    book.children[0].children[i].draggable = "true";
    //加入ondragstart事件
    book.children[0].children[i].ondragstart = drag;

    option = document.createElement("option");//增加物件option
    option.value = book.children[0].children[i].title;//寫入值
    option.innerText = book.children[0].children[i].innerHTML;//寫入內文
    music.appendChild(option);//增加子節點，要放在後面
}
chngMusic(0);


function updateMusic() {
    //1. 把music原先的option全部移除
    for (i = music.children.length - 1; i >= 0; i--) {//用迴圈刪除歌曲，必須從尾巴開始才不會遺漏
        music.removeChild(music.children[i]);//也能用music.remove(i)
    }

    //2. 再讀取我的歌本內的歌曲,append至music下拉選單
    for (i = 0; i < book.children[1].children.length; i++) {
        option = document.createElement("option");//增加物件option
        option.value = book.children[1].children[i].title;//寫入值
        option.innerText = book.children[1].children[i].innerHTML;//寫入內文
        music.appendChild(option);//增加子節點，要放在後面
    }

    chngMusic(0);

}

//選擇歌曲/上下一首
function chngMusic(i) {
    audio.children[0].src = music.options[music.selectedIndex + i].value;
    audio.children[0].title = music.options[music.selectedIndex + i].innerText;
    music.options[music.selectedIndex + i].selected = true;
    audio.load();
    if (btnPlay.innerHTML == ";") { playSong() };
}

//轉換歌曲時間為分與秒

min = 0, sec = 0;
function getTimeFormat(timeSec) {
    min = parseInt(timeSec / 60);
    sec = parseInt(timeSec % 60);
    //if...else...三元表達式
    min = min < 10 ? "0" + min : min;
    sec = sec < 10 ? "0" + sec : sec;
    return min + ":" + sec;
}

//取得目前播放時間
var w = 0;
var r = 0;//隨機播放歌曲的亂數值
getDuration();
function getDuration() {
    info.children[1].innerText = getTimeFormat(audio.currentTime) + "/" + getTimeFormat(audio.duration);
    w = audio.currentTime / audio.duration * 100;
    progress.value = parseInt(audio.currentTime);
    progress.max = parseInt(audio.duration);
    progress.style.backgroundImage = "-webkit-linear-gradient(left,#b40000,#b40000 " + w + "%, #a5ffff " + w + "% ,#a5ffff)";
    //console.log(w);
    //歌播完換下一首
    if (audio.currentTime == audio.duration) {
        if (info.children[2].innerText == "單曲循環") {
            chngMusic(0);
        }
        else if (info.children[2].innerText == "隨機播放") {
            r = Math.floor(Math.random() * music.options.length);//抓亂數的索引值後，減掉目前索引位置，才能得到正確的增加值
            console.log(r);
            r = r - music.selectedIndex;
            chngMusic(r);
        }
        else if (music.selectedIndex == music.options.length - 1) { //如果是最後一首，就停止播放
            if (info.children[2].innerText == "循環播放") {
                r = - music.selectedIndex;
                chngMusic(r);
            }
            else stopSong();
        }
        else chngMusic(1);//如果都不是，就下一首
    }
    setTimeout(getDuration, 50);
}
//播放音樂
function playSong() {
    audio.play();
    btnPlay.innerText = ";";
    btnPlay.onclick = pauseSong;
    info.children[0].innerText = "現在播放:" + audio.children[0].title;

}
//音樂暫停
function pauseSong() {
    audio.pause();
    btnPlay.innerText = "4";
    btnPlay.onclick = playSong;
    info.children[0].innerText = "暫停播放";
}
//音樂停止
function stopSong() {
    pauseSong();
    audio.currentTime = 0;
    info.children[0].innerText = "停止播放";
}
//快轉及倒轉
function chngTime(sec) {
    audio.currentTime += sec;
}
//播放進度條拖曳
function setProgress() {
    audio.currentTime = progress.value;
}

//單曲循環
function sameSong() {
    info.children[2].innerText = info.children[2].innerText == "單曲循環" ? "正常模式" : "單曲循環";
}
//循環播放
function cycleSong() {
    info.children[2].innerText = info.children[2].innerText == "循環播放" ? "正常模式" : "循環播放";
}
//隨機播放
function randomSong() {
    info.children[2].innerText = info.children[2].innerText == "隨機播放" ? "正常模式" : "隨機播放";
}

//靜音
function setMuted() {
    audio.muted = !audio.muted;
    btnMuted.style.textDecoration = audio.muted ? "line-through" : "none";
}

setVol();
//音量調整
function setVol() {
    volValue.value = setVolValue.value;
    audio.volume = setVolValue.value / 100;
    setVolValue.style.backgroundImage = "-webkit-linear-gradient(left,#ef7d2c,#ef7d2c " + setVolValue.value + "%, #c8c8c8 " + setVolValue.value + "% ,#c8c8c8)";
}

//音量微調
function adjVol(vol) {
    setVolValue.value = parseInt(setVolValue.value) + vol;
    setVol();
}
//當來源物件被拖曳到目標區上方時呼叫他
function allowDrop(ev) {
    ev.preventDefault();//停止物件預設行為
}
//當來源物件被拖曳時呼叫它
function drag(ev) {
    ev.dataTransfer.setData("text", ev.target.id);
}

//當來源物件被丟到目標區時呼叫他
function drop(ev) {
    ev.preventDefault();
    var data = ev.dataTransfer.getData("text");
    if (ev.target.id == "") {
        ev.target.appendChild(document.getElementById(data));
    } else
        ev.target.parentNode.appendChild(document.getElementById(data));
}

//歌單介面

function showBook() {
    book.className = book.className == "" ? "hide" : "";
}