document.querySelectorAll('.menu-item').forEach(item => {
    item.addEventListener('click', function(e) {
        e.preventDefault();

        // 先隱藏所有B2-content區塊
        document.querySelectorAll('.B2-content').forEach(content => {
            content.style.display = 'none';
        });

        const content = this.getAttribute('data-content');

        switch(content) {
            case 'intro':
                document.getElementById('intro-section').style.display = 'block';
                break;
            case 'activity':
                document.getElementById('activity-section').style.display = 'block';
                break;
            case 'settings':
                document.getElementById('settings-section').style.display = 'block';
                break;
            case 'delete':
                document.getElementById('delete-section').style.display = 'block';
                break;
        }
    });
});
