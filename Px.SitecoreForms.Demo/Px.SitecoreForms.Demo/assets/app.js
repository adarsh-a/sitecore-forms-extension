const checkRadio = () => {
  const allCheckboxes = document.querySelectorAll('input[type="checkbox"]');
  const allRadio = document.querySelectorAll('input[type="radio"]');

  allCheckboxes.forEach((check) => {
    const newSpan = document.createElement('span');
    check.parentNode.appendChild(newSpan);
    check.parentNode.classList.add('checkbox');
  });

  allRadio.forEach((radio) => {
    const newSpan = document.createElement('span');
    radio.parentNode.appendChild(newSpan);
    radio.parentNode.classList.add('radio');
  });
};

const ratingOverview = () => {
  const allOverviews = document.querySelectorAll('.rating-overview');
  allOverviews.forEach((element) => {
    const allRows = element.querySelectorAll('.rate-row');

    let total = 0;

    allRows.forEach((row) => {
      total += parseInt(row.getAttribute('data-value'), 0);
    });

    allRows.forEach((row) => {
      const value = row.getAttribute('data-value');
      const percent = parseFloat((value / total) * 100).toFixed(2);
      const bar = row.querySelector('.bar');
      bar.style.width = `${percent}%`;
    });
  });
};

const accordion = () => {
  const accordionTabs = document.querySelectorAll('.accordion .row');
  accordionTabs.forEach((accordionTab) => {
    accordionTab.addEventListener('click', () => {
      accordionTab.firstChild.classList.toggle('active');
    });
  });
};

const filters = () => {
  const firstParent = document.querySelector('.filters-title');
  firstParent.addEventListener('click', () => {
    firstParent.classList.toggle('expanded');
    const childContainer = document.querySelector('.filters-content');
    childContainer.classList.toggle('active');
    const childsOfFilter = document.querySelectorAll('.row');
    const elements = document.querySelectorAll('.span');
    for (let index = 0; index < elements.length; index += 1) {
      elements[index].addEventListener('click', () => {
        childsOfFilter[index].classList.toggle('expanded');
      });
    }
  });
};

const ingredientsToggle = () => {
  const allIngredientsModules = document.querySelectorAll('.ingredients');

  allIngredientsModules.forEach((element) => {
    const list = element.querySelector('ul');
    const listItems = element.querySelectorAll('li');
    const toggle = element.querySelector('.toggle-ingredients');

    toggle.addEventListener('click', () => {
      list.classList.toggle('open');
      toggle.classList.toggle('open');
    });

    if (listItems.length <= 4) {
      toggle.remove();
    }

    listItems.forEach((item, index) => {
      if (index > 3) {
        item.classList.toggle('hide');
      }
    });
  });
};

function closeAllSelect(element) {
  const elementsToHide = [];
  const options = document.getElementsByClassName('select-items');
  const optionSelected = document.getElementsByClassName('select-selected');
  let i = 0;
  for (i; i < optionSelected.length; i += 1) {
    if (element === optionSelected[i]) {
      elementsToHide.push(i);
    } else {
      optionSelected[i].classList.remove('select-arrow-active');
    }
  }
  for (i = 0; i < options.length; i += 1) {
    if (elementsToHide.indexOf(i)) {
      options[i].classList.add('select-hide');
    }
  }
}

const sortsToggle = () => {
  const customSelect = document.querySelector('.custom-select');
  const selElmnt = customSelect.getElementsByTagName('select')[0];
  const newElmnt = document.createElement('DIV');
  newElmnt.setAttribute('class', 'select-selected');
  newElmnt.innerHTML = selElmnt.options[selElmnt.selectedIndex].innerHTML;
  customSelect.appendChild(newElmnt);
  const optionList = document.createElement('DIV');
  optionList.setAttribute('class', 'select-items select-hide');
  let optionsLength = 0;
  for (optionsLength; optionsLength < selElmnt.length; optionsLength += 1) {
    const optionItem = document.createElement('DIV');
    optionItem.innerHTML = selElmnt.options[optionsLength].innerHTML;
    optionItem.addEventListener('click', function reducer() {
      let sameAsSelected;
      let selectedLength = 0;
      const parent = this.parentNode.parentNode.getElementsByTagName('select')[0];
      const previous = this.parentNode.previousSibling;
      let parentLength = 0;
      for (parentLength; parentLength < parent.length; parentLength += 1) {
        if (parent.options[parentLength].innerHTML === this.innerHTML) {
          parent.selectedIndex = parentLength;
          previous.innerHTML = this.innerHTML;
          sameAsSelected = this.parentNode.getElementsByClassName('same-as-selected');
          for (selectedLength = 0; selectedLength < sameAsSelected.length; selectedLength += 1) {
            sameAsSelected[selectedLength].removeAttribute('class');
          }
          this.setAttribute('class', 'same-as-selected');
          break;
        }
      }
      previous.click();
    });
    optionList.appendChild(optionItem);
  }
  customSelect.appendChild(optionList);
  newElmnt.addEventListener('click', function reducer(e) {
    e.stopPropagation();
    closeAllSelect(this);
    this.nextSibling.classList.toggle('select-hide');
    this.classList.toggle('select-arrow-active');
  });
};

const modalToggle = () => {
  const overlay = document.querySelector('.overlay');
  if (overlay !== null) {
    overlay.addEventListener('click', () => {
      const modalOpen = document.getElementById('myModal');
      modalOpen.classList.remove('open');
    });
  }

  const allTriggers = document.querySelectorAll('.modal-toggle');

  for (let index = 0; index < allTriggers.length; index += 1) {
    const element = allTriggers[index];
    const modalId = element.getAttribute('data-modal');
    element.addEventListener('click', () => {
      const modal = document.getElementById(modalId);
      const video = modal.querySelector('.video-container');
      modal.classList.toggle('open');
      if (video) {
        modal.classList.toggle('video-modal');
        video.innerHTML = `<iframe src="https://www.youtube.com/embed/${video.getAttribute('data-video')}" frameBorder="0" allow="accelerometer; autoplay; encrypted-media; gyroscope; picture-in-picture" allowFullScreen />`;
      }
    });
  }
};


function toggleExample(el) {
  const classes = el.classList.value;
  if (classes.indexOf('red') > -1) {
    el.classList.add('yellow');
    el.classList.remove('red');
  } else {
    el.classList.add('red');
    el.classList.remove('yellow');
  }
}

function menuToggle() {
  const header = document.querySelector('header');
  header.classList.toggle('close');
}

function openSubMenu(el) {
  const target = el.parentNode;
  target.classList.toggle('open');
}

function toggleSearch(el) {
  el.classList.toggle('open');
}


document.addEventListener('click', (event) => {
  const el = event.target;
  if (el.matches('.toggle')) {
    toggleExample(el);
  } else if (el.matches('.menu-toggle')) {
    menuToggle();
  } else if (el.matches('.menu-item.has-sub a')) {
    openSubMenu(el);
  } else if (el.matches('.search')) {
    toggleSearch(el);
  }
}, false);

export {
  accordion,
  ratingOverview,
  ingredientsToggle,
  sortsToggle,
  filters,
  modalToggle,
  checkRadio,
};
