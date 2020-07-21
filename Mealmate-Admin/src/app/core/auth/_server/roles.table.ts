export class RolesTable {
  public static roles: any = [
    {
      id: 1,
      title: "Administrator",
      isCoreRole: true,
      permissions: [1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16],
    },
    {
      id: 2,
      title: "Operator",
      isCoreRole: false,
      permissions: [3, 4, 10],
    },
    {
      id: 3,
      title: "Waiter",
      isCoreRole: false,
      permissions: [],
    },
  ];
}
