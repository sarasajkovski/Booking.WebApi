CREATE TABLE "Room" (
    id SERIAL PRIMARY KEY,
    name VARCHAR(100) NOT NULL,
    capacity INT NOT NULL,
    "roomType" VARCHAR(50) NOT NULL,
    "isAvailable" BOOLEAN NOT NULL
);

CREATE TABLE "Reservation" (
    id SERIAL PRIMARY KEY,
    room_id INT NOT NULL,
    "fullName" VARCHAR(100) NOT NULL,
    "reservationDate" DATE NOT NULL,
    "isAvailable" BOOLEAN NOT NULL,

    FOREIGN KEY (room_id)
    REFERENCES "Room"(id)
);

INSERT INTO "Room" (name, capacity, "roomType", "isAvailable")
VALUES
('Conference Room', 10, 'Conference', true),
('Meeting Room', 5, 'Meeting', false),
('Event Hall', 50, 'Event', true);


