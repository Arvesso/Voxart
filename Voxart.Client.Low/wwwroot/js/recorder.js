let recorder;
let audioContext;
let audioStream;

async function startRecording() {
    audioStream = await navigator.mediaDevices.getUserMedia({ audio: true });
    audioContext = new AudioContext();
    let input = audioContext.createMediaStreamSource(audioStream);
    recorder = new Recorder(input, { numChannels: 1 });
    recorder.record();
}

function stopRecording() {
    return new Promise((resolve, reject) => {
        recorder.stop();
        audioStream.getTracks().forEach(track => track.stop());

        recorder.exportWAV(blob => {
            let reader = new FileReader();
            reader.onload = function (event) {
                let arrayBuffer = event.target.result;
                let audioBytes = new Uint8Array(arrayBuffer);
                resolve(audioBytes);
            };
            reader.readAsArrayBuffer(blob);
        });
    });
}