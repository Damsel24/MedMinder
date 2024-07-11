import React from 'react';

export default function Table() {
  return (
    <div className="flex flex-col">
      <div className="p-1 md:p-8 overflow-x-auto sm:-mx-6 lg:-mx-8 shadow-md"> {/* Added padding (p-4 and md:p-8) */}
        <div className="inline-block min-w-full py-2 sm:px-6 ">
          <div className="overflow-hidden">
            <table className="min-w-full text-center text-sm font-light bg-white rounded-lg overflow-hidden shadow-md">
              <thead className="border-b-2 border-gray-300 bg-neutral-50 font-medium dark:border-neutral-500 dark:text-neutral-800">
                <tr>
                  <th scope="col" className="px-6 py-4">#</th>
                  <th scope="col" className="px-6 py-4">First</th>
                  <th scope="col" className="px-6 py-4">Last</th>
                  <th scope="col" className="px-6 py-4">Handle</th>
                </tr>
              </thead>
              <tbody>
                <tr className="border-b-2 border-gray-300 dark:border-neutral-500 hover:bg-blue-100 dark:hover:bg-blue-800">
                  <td className="whitespace-nowrap px-6 py-4 font-medium">1</td>
                  <td className="whitespace-nowrap px-6 py-4">Mark</td>
                  <td className="whitespace-nowrap px-6 py-4">Otto</td>
                  <td className="whitespace-nowrap px-6 py-4">@mdo</td>
                </tr>
                <tr className="border-b-2 border-gray-300 dark:border-neutral-500 hover:bg-blue-100 dark:hover:bg-blue-800">
                  <td className="whitespace-nowrap px-6 py-4 font-medium">2</td>
                  <td className="whitespace-nowrap px-6 py-4 ">Jacob</td>
                  <td className="whitespace-nowrap px-6 py-4">Thornton</td>
                  <td className="whitespace-nowrap px-6 py-4">@fat</td>
                </tr>
                <tr className="border-b-2 border-gray-300 dark:border-neutral-500 hover:bg-blue-100 dark:hover:bg-blue-800">
                  <td className="whitespace-nowrap px-6 py-4 font-medium">3</td>
                  <td colSpan={2} className="whitespace-nowrap px-6 py-4">
                    Larry the Bird
                  </td>
                  <td className="whitespace-nowrap px-6 py-4">@twitter</td>
                </tr>
              </tbody>
            </table>
          </div>
        </div>
      </div>
    </div>
  );
}
