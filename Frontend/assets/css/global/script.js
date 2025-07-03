//BARRA EXPANDIDA


const header = document.querySelector("header");
window.addEventListener("scroll", () => {
    if (window.scrollY > 50) {
        header.classList.add("expandido");
    } else {
        header.classList.remove("expandido");
        console.log("Voltou ao normal.");
    }

});

