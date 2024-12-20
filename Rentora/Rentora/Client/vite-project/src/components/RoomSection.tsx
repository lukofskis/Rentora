import { getRooms } from "../api/houseApi.tsx";
import { useQuery } from "@tanstack/react-query";
import LoadingSpinner from "./LoadingSpinner";
import { Link } from "react-router-dom";
import RoomCard from "./RoomCard";

type Props = { houseId: string };

const RoomSection = ({ houseId }: Props) => {
    const { data: rooms, isLoading } = useQuery({
        queryKey: ["rooms", { houseId }],
        queryFn: () => getRooms(houseId),
    });

    if (isLoading) {
        return (
            <div className="flex justify-center items-center min-h-screen">
                <LoadingSpinner />
            </div>
        );
    }

    return (
        <div>
            <hr className="my-8 border-t-6 border-purple-600" />
            <h2 className="text-2xl font-semibold text-center mb-6">Rooms</h2>

            {rooms === null || rooms?.length === 0 ? (
                <div className="text-center text-xl text-gray-400">
                    <p>No Rooms Found</p>
                </div>
            ) : (
                <div className="p-6 grid grid-cols-1 sm:grid-cols-2 md:grid-cols-3 gap-6">
                    {rooms?.map((room) => {
                        return (
                            <Link key={room.id} to={`/house/${houseId}/room/${room.id}`}>
                                <RoomCard room={room} />
                            </Link>
                        );
                    })}
                </div>
            )}
        </div>
    );
};

export default RoomSection;
