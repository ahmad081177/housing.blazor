var gmap;
var infoWindow;

(g => { var h, a, k, p = "The Google Maps JavaScript API", c = "google", l = "importLibrary", q = "__ib__", m = document, b = window; b = b[c] || (b[c] = {}); var d = b.maps || (b.maps = {}), r = new Set, e = new URLSearchParams, u = () => h || (h = new Promise(async (f, n) => { await (a = m.createElement("script")); e.set("libraries", [...r] + ""); for (k in g) e.set(k.replace(/[A-Z]/g, t => "_" + t[0].toLowerCase()), g[k]); e.set("callback", c + ".maps." + q); a.src = `https://maps.${c}apis.com/maps/api/js?` + e; d[q] = f; a.onerror = () => h = n(Error(p + " could not load.")); a.nonce = m.querySelector("script[nonce]")?.nonce || ""; m.head.append(a) })); d[l] ? console.warn(p + " only loads once. Ignoring:", g) : d[l] = (f, ...n) => r.add(f) && u().then(() => d[l](f, ...n)) })({
    key: gmap_key,
    v: "weekly",
    // Use the 'v' parameter to indicate the version to use (weekly, beta, alpha, etc.).
    // Add other bootstrap parameters as needed, using camel case.
});

export async function initMap(lat = -34.397, lon = 150.644, zoom = 10) {
    const { Map } = await google.maps.importLibrary("maps");
    const { AdvancedMarkerElement, PinElement } = await google.maps.importLibrary(
        "marker",
    );
    gmap = new Map(document.getElementById("gmap"), {
        center: { lat: lat, lng: lon },
        zoom: zoom,
        mapId: "HousingWebApp",
    });

    infoWindow = new google.maps.InfoWindow();
}

export async function show_houses(houses) {
    // houses is a list of houses
    for (var i = 0; i < length; i++) {
        var house = houses[i];
        var latLng = new google.maps.LatLng(house.address.lat, house.address.longt);
        // var marker = new google.maps.Marker({
        //     position: latLng,
        //     map: gmap,
        //     title: house.info
        // });

        const iconImage = document.createElement("img");
        iconImage.src = "http://maps.gstatic.com/mapfiles/ms2/micons/homegardenbusiness.png";
        var marker = new google.maps.marker.AdvancedMarkerElement({
            position: latLng,
            map: gmap,
            title: house.info,
            content: iconImage
        });
        var contentString = `<div id="content info-window">
            <h2>${house.info}</h2>
            <hr/>
            <img src="${house.mainImage}" alt="House Image" style="width:100%; height: auto;">
            <hr/>
            <a href="/house/${house.id}" class="btn btn-primary">Details</a>
        </div>`;

        makeInfoWindow(gmap, infoWindow, contentString, marker);
    }
}
export function makeInfoWindow(map, infowindow, contentString, marker) {
    google.maps.event.addListener(marker, "click", function() {
        infowindow.setContent(contentString);
        infowindow.open(map, marker);
    });
}