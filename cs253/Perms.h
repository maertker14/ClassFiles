
//Credit to Jack Applin of Colorado State University
template <typename T, typename U>
class Perms {
    const T t;
    const U u;
  public:
    Perms(const T &tt, const U &uu) : t(tt), u(uu) { }
    operator T() const { return t; }
    operator U() const { return u; }
};//end credit of Jack Applin
