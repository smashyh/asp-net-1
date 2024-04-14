document.addEventListener('DOMContentLoaded', () => {
    try {

        let fileUploader = document.querySelector('#file-uploader');
        fileUploader.addEventListener('change', (e) => {
            if (e.target.files.length > 0) {
                e.target.form.submit();
            }
        });

    }
    catch { }
});