import { useQuery } from "@tanstack/react-query";
import { getHouses } from "../api/houseApi.tsx";
import HouseCard from "../components/HouseCard.tsx";
import LoadingSpinner from "../components/LoadingSpinner";
import { Link } from "react-router-dom";
import { useAuth } from "../components/AuthProvider";

const HomePage = () => {
    const { data: houses, isLoading } = useQuery({
        queryKey: ["houses"],
        queryFn: () => getHouses(),
    });

    const { currentUser } = useAuth();

    if (isLoading) {
        return (
            <div className="flex justify-center items-center min-h-screen">
                <LoadingSpinner />
            </div>
        );
    }

    const isAdmin = currentUser?.groups.includes("Admin");

    return (
        <div className="relative">
            {isAdmin && (
                <Link
                    to="/house"
                    className="absolute top-4 right-4 bg-green-600 text-white p-4 rounded-full shadow-lg hover:bg-green-700 transition duration-300"
                >
                    <span className="text-2xl">+</span>
                </Link>
            )}

            {houses === null || houses?.length === 0 ? (
                <div className="flex justify-center items-center mt-20">
                    <p className="text-xl text-gray-400">No Houses Found</p>
                </div>
            ) : (
                <div className="p-6 grid grid-cols-1 sm:grid-cols-2 md:grid-cols-3 gap-6">
                    {houses?.map((house) => (
                        <Link key={house.id} to={`/house/${house.id}`}>
                            <HouseCard house={house} />
                        </Link>
                    ))}
                </div>
            )}
        </div>
    );
};

export default HomePage;