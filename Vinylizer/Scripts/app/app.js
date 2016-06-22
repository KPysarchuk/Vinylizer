$(document).ready(function () {
    console.log(1);
    var trackName = getCookie("VinylizerFileName");

    $("#filterVolume").change(function () {
        console.log(trackName, this.value);

        var audio = $("#audio");
        $("#mp3_src").attr("src", "/Home/GetFilterForPlay?volumeLvl=" + this.value + "&fileName=" + trackName);
        audio[0].pause();
        audio[0].load();//suspends and restores all audio element

        //audio[0].play(); changed based on Sprachprofi's comment below
        audio[0].oncanplaythrough = audio[0].play();
    });

});


function getCookie(name) {
    var matches = document.cookie.match(new RegExp(
      "(?:^|; )" + name.replace(/([\.$?*|{}\(\)\[\]\\\/\+^])/g, '\\$1') + "=([^;]*)"
    ));
    return matches ? decodeURIComponent(matches[1]) : undefined;
}