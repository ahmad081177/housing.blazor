var map;
export function map_init(lat = 33, lon = 33, zoom = 10) {
    map = L.map('map').
        setView([lat, lon], zoom);

    L.tileLayer('https://tile.openstreetmap.org/{z}/{x}/{y}.png', {
        maxZoom: 19,
        attribution: '&copy; <a href="http://www.openstreetmap.org/copyright">OpenStreetMap</a>'
    }).addTo(map);
}

export function show_houses(houses)
{
    console.log(houses);
    for (var i = 0; i < houses.length; i++)
    {
        let house = houses[i];
        let address = house.address;
        //var marker = L.marker([address.lat, address.lon]).addTo(map);
        var redmarker = L.ExtraMarkers.icon({
            icon: 'io-home',
            prefix: 'ion',
            markerColor: 'blue',
            shape: 'square',
        });
        var marker = L.marker([address.lat, address.longt],
                            { icon: redmarker }).addTo(map);
        let h = house2html(house);
        marker.bindPopup(h);
        marker.house = house;
        marker.address = address;
    };
}

export function house2html(house) {
    let h = `<b>${house.info}</b><br>`;
    h += '<hr>';
    h += `<img src="${house.mainImage}" width="200" height="200">`;
    h += '<hr>';
    h += `<b>Price:</b> ${house.price}<br>`;
    h += `<button onclick="window.location.href='/house/${house.id}'">Details...</button>`;
    return h;
}