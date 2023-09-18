document.addEventListener('DOMContentLoaded', function() {
    adjustArticleLayout();
});

function adjustArticleLayout() {
    const articles = document.querySelectorAll('.article');
    const container = document.getElementById('article-container');

    const columnCount = 3;
    const columnHeights = Array.from({ length: columnCount }, () => 0);

    articles.forEach(function(article) {
        const minHeight = Math.min(...columnHeights);
        const columnIndex = columnHeights.indexOf(minHeight);
        article.style.position = 'absolute';
        article.style.left = (article.offsetWidth + 20) * columnIndex + 'px';
        article.style.top = minHeight + 'px';
        columnHeights[columnIndex] += article.offsetHeight + 20;
    });

    const maxHeight = Math.max(...columnHeights);
    container.style.height = maxHeight + 'px';
}
