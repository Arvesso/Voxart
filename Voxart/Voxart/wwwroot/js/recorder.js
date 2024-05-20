let _mediaRecorder;
let audioChunks = [];
let mediaStream;

async function startRecording() {
    mediaStream = await navigator.mediaDevices.getUserMedia({ audio: true });
    _mediaRecorder = new MediaRecorder(mediaStream);
    _mediaRecorder.ondataavailable = (event) => {
        audioChunks.push(event.data);
    };
    _mediaRecorder.start();
}

function stopRecording() {
    return new Promise((resolve, reject) => {
        _mediaRecorder.onstop = async () => {
            let audioBlob = new Blob(audioChunks, { type: 'audio/wav' });
            let arrayBuffer = await audioBlob.arrayBuffer();
            let audioBytes = new Uint8Array(arrayBuffer);
            audioChunks = [];
            mediaStream.getTracks().forEach(track => track.stop());
            resolve(audioBytes);
        };
        _mediaRecorder.stop();
    });
}