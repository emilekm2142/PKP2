package yeah.hack.filizanka.repository;

import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.stereotype.Repository;
import org.springframework.transaction.annotation.Propagation;
import org.springframework.transaction.annotation.Transactional;
import yeah.hack.filizanka.model.Skin;

@Repository
@Transactional(propagation = Propagation.MANDATORY)
public interface SkinRepository extends JpaRepository<Skin, Long> {
}
