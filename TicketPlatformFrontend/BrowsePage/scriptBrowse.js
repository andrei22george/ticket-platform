function add_remove_wishlist(id){
    var ev = document.getElementById(id);
    var starDiv = ev.querySelector('.wishlist-btn');
    
    if (starDiv.textContent === '☆') {
        starDiv.textContent = '★';
        // Add to wishlist or perform other actions
    } else {
        
        starDiv.textContent = '☆'; 
        // Remove from wishlist or perform other actions
    }
    
}

