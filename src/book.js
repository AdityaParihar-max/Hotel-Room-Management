'use strict';
// Modals
const modal = document.querySelector('.modal');
const overlay = document.querySelector('.overlay');
const btnCloseModal = document.querySelector('.btn--close-modal');
const btnOpenModal = document.querySelectorAll('.btn--show-modal');
const nav = document.querySelector('.nav');

const openModal = function (e) {
  e.preventDefault();
  modal.classList.remove('hidden');
  overlay.classList.remove('hidden');
};

const closeModal = function () {
  modal.classList.add('hidden');
  overlay.classList.add('hidden');
};
btnOpenModal.forEach(btn => btn.addEventListener('click', openModal));

btnCloseModal.addEventListener('click', closeModal);
overlay.addEventListener('click', closeModal);

document.addEventListener('keydown', function (e) {
  if (e.key === 'Escape' && !modal.classList.contains('hidden')) {
    closeModal();
  }
});
// API fetch and post form

//Naviagtion
document.querySelectorAll('.nav__link').forEach(function (el) {
  el.addEventListener('click', function (e) {
    e.preventDefault();
    const id = this.getAttribute('href');
    document.querySelector(id).scrollIntoView({
      behavior: 'smooth',
    });
  });
});

//Menu fade animation
const handleHover = function (e) {
  if (e.target.classList.contains('nav__link')) {
    const link = e.target;
    const siblings = link.closest('.nav').querySelectorAll('.nav__link');
    const logo = link.closest('.nav').querySelector('img');
    siblings.forEach(el => {
      if (el !== link) el.style.opacity = this;
      logo.style.opacity = this;
    });
  }
};
nav.addEventListener('mouseover', handleHover.bind(0.5));
nav.addEventListener('mouseout', handleHover.bind(1));
// ---------------------------------------------------------------------------
document.getElementById('bookingForm').addEventListener('submit', async e => {
  e.preventDefault();

  const formData = new FormData(e.target);
  const data = Object.fromEntries(formData.entries());

  const resultDiv = document.getElementById('result');
  resultDiv.style.display = 'block';
  resultDiv.innerHTML = ''; // clear previous content

  try {
    const response = await fetch(
      'http://localhost:5192/api/Rooms/check-availability',
      {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ roomType: data.RoomType }), // use camelCase if that's what your API expects
      }
    );

    if (response.ok) {
      const room = await response.json();

      if (Array.isArray(room) && room.length > 0) {
        let html = '<strong>Rooms Available:</strong><br><br>';

        room.forEach(r => {
          html += `
      <div style="margin-bottom: 1rem; padding: 0.5rem; border-bottom: 1px solid #ccc;">
        <strong>Room Number:</strong> ${r.roomNumber}<br>
        <strong>Type:</strong> ${r.type}<br>
        <strong>Price:</strong> ₹${r.price}<br>
        <strong>Features:</strong> ${r.features}
      </div>
    `;
        });

        resultDiv.innerHTML = html;
        resultDiv.style.backgroundColor = '#e9f7ef';
        resultDiv.style.color = '#155724';
      } else {
        resultDiv.innerHTML = '<strong>No available room found.</strong>';
        resultDiv.style.backgroundColor = '#f8d7da';
        resultDiv.style.color = '#721c24';
      }
    }
  } catch (err) {
    console.error(err);
    resultDiv.innerHTML = '<strong>Error connecting to the server.</strong>';
    resultDiv.style.backgroundColor = '#f8d7da';
    resultDiv.style.color = '#721c24';
  }
});
