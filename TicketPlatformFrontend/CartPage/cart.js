document.addEventListener("visibilitychange", function () {
    if (document.hidden) {
      document.title = "Finish your purchase!";
    } else {
      document.title = "Ticket cart";
    }
  });